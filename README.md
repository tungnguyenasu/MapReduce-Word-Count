# 🧠 MapReduce Web Application

This ASP.NET WebForms-based project performs **automated MapReduce computation** by allowing a user to upload a text file, specify a number of parallel threads, and define service endpoints for **Map**, **Reduce**, and **Combiner** functions. It simulates distributed word counting using a service-oriented architecture and multithreading.

## 🚀 Features

- ✅ Upload `.txt` files for processing
- 🔁 Splits input data into parallel chunks
- 🗺️ Sends data to external `Map`, `Reduce`, and `Combiner` services via HTTP
- 📊 Displays final word count result in a formatted output
- 🧪 Input validation and real-time upload status messages
- 🔐 Session-based temporary storage of uploaded data

## 📂 Project Structure

MapReduceWebApp/
├── Default.aspx # Main UI layout (file upload, inputs, output)
├── Default.aspx.cs # Code-behind: MapReduce logic and event handling
├── Web.config # ASP.NET configuration
├── bin/ # Build output directory
├── obj/ # Intermediate build files
└── App_Data/ # Optional: internal data store (if needed)

## ⚙️ How It Works

1. **User Uploads File**: `.txt` file is validated and read into session.
2. **Thread Count**: User specifies number `N` for parallel processing.
3. **Web Service Endpoints**:
   - `Map`: Tokenizes and counts words
   - `Reduce`: Aggregates intermediate results
   - `Combiner`: Merges final results
4. **MapReduce Execution**: Once triggered, tasks are distributed across threads and output is shown.

## 🖥️ Usage

1. Run the project in **Visual Studio** with IIS Express.
2. Open the browser and navigate to `http://localhost:PORT/Default.aspx`
3. Choose a `.txt` file and click **Upload**.
4. Enter:
   - N (number of parallel threads)
   - Map service URL (e.g., `https://localhost:44331/map`)
   - Reduce service URL
   - Combiner service URL
5. Click **Perform MapReduce Computation** to run.

💡 Technologies Used

ASP.NET WebForms (.NET Framework 4.8)

C# (async/await, HttpClient, Session)

HTML/CSS

RESTful JSON Web Services

Multithreading Simulation

🔐 Security Notes

File upload only accepts .txt files

No files are written to disk — content is stored in session

🛠️ Setup
git clone https://github.com/YOUR_USERNAME/MapReduceWebApp.git
cd MapReduceWebApp
