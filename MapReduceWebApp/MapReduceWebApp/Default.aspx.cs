using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MapReduceWebApp
{
    public partial class _Default : System.Web.UI.Page
    {
        // Handles the MapReduce computation when the "Perform MapReduce" button is clicked
        protected async void ButtonStart_Click(object sender, EventArgs e)
        {
            // Retrieve uploaded file content from Session
            string fileContent = Session["UploadedText"] as string;

            if (string.IsNullOrEmpty(fileContent))
            {
                // If no content is available, show error message
                LiteralOutput.Text = "<span style='color:red;'>❌ Please upload a file.</span>";
                return;
            }

            // Extract parameters from input controls
            int threadCount = int.TryParse(TextBoxThreads.Text, out int n) ? Math.Max(1, n) : 1;
            string mapUrl = TextBoxMapUrl.Text.Trim();
            string reduceUrl = TextBoxReduceUrl.Text.Trim();
            string combinerUrl = TextBoxCombinerUrl.Text.Trim();

            // Split the file content into chunks for parallel Map tasks
            var chunks = SplitText(fileContent, threadCount);

            // Initialize the HTTP client for service calls
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Step 1: Map - send each chunk to the Map service
                var mapTasks = chunks.Select(chunk =>
                    httpClient.PostAsJsonAsync(mapUrl, chunk).Result.Content.ReadAsAsync<Dictionary<string, int>>()
                ).ToArray();

                await Task.WhenAll(mapTasks);
                var mapResults = mapTasks.Select(t => t.Result).ToList();

                // Step 2: Reduce - reduce each mapped result
                var reduceTasks = mapResults.Select(map =>
                    httpClient.PostAsJsonAsync(reduceUrl, new List<Dictionary<string, int>> { map }).Result.Content.ReadAsAsync<Dictionary<string, int>>()
                ).ToArray();

                await Task.WhenAll(reduceTasks);
                var reduced = reduceTasks.Select(t => t.Result).ToList();

                // Step 3: Combine - send reduced results to the Combiner service
                var combineResponse = await httpClient.PostAsJsonAsync(combinerUrl, reduced);
                var finalResult = await combineResponse.Content.ReadAsAsync<Dictionary<string, int>>();

                // Display final word count result
                LiteralOutput.Text = "<h3>✅ Final Word Count Result:</h3><pre>" +
                    string.Join("\n", finalResult.OrderByDescending(kv => kv.Value)
                    .Select(kv => $"{kv.Key}: {kv.Value}")) + "</pre>";
            }
            catch (Exception ex)
            {
                // Catch any errors from async calls or network issues
                LiteralOutput.Text = $"<span style='color:red;'>❌ Error: {ex.Message}</span>";
            }
        }

        // Splits the uploaded text into roughly equal-sized chunks for parallel processing
        private List<string> SplitText(string text, int parts)
        {
            var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            int chunkSize = (int)Math.Ceiling(lines.Count / (double)parts);

            return Enumerable.Range(0, parts)
                             .Select(i => string.Join(" ", lines.Skip(i * chunkSize).Take(chunkSize)))
                             .Where(chunk => !string.IsNullOrWhiteSpace(chunk))
                             .ToList();
        }

        // Handles file upload validation and stores the uploaded text into session
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            // Check if a file is selected
            if (!FileUpload1.HasFile)
            {
                LabelUploadStatus.ForeColor = System.Drawing.Color.Red;
                LabelUploadStatus.Text = "❌ Please choose a file to upload.";
                Session["UploadedText"] = null;
                return;
            }

            // Validate file extension
            string extension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();

            if (extension != ".txt")
            {
                LabelUploadStatus.ForeColor = System.Drawing.Color.Red;
                LabelUploadStatus.Text = "❌ Invalid file type. Please upload a .txt file.";
                Session["UploadedText"] = null;
                return;
            }

            try
            {
                // Read file content and store it in Session
                FileUpload1.PostedFile.InputStream.Position = 0;
                string content = new StreamReader(FileUpload1.PostedFile.InputStream).ReadToEnd();
                Session["UploadedText"] = content;

                // Display success message
                LabelUploadStatus.ForeColor = System.Drawing.Color.Green;
                LabelUploadStatus.Text = "✅ File ready. You can now run MapReduce.";
            }
            catch (Exception ex)
            {
                // Handle any file read errors
                LabelUploadStatus.ForeColor = System.Drawing.Color.Red;
                LabelUploadStatus.Text = "❌ Error reading file: " + ex.Message;
                Session["UploadedText"] = null;
            }
        }
    }
}
