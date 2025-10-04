using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace MapService.Controllers
{
    public class MapController : ApiController
    {
        [HttpPost]
        [Route("map")]
        public Dictionary<string, int> Map([FromBody] string input)
        {
            var counts = new Dictionary<string, int>(System.StringComparer.OrdinalIgnoreCase);
            var words = Regex.Split(input, @"\W+");

            foreach (var word in words)
            {
                if (string.IsNullOrWhiteSpace(word)) continue;
                if (counts.ContainsKey(word))
                    counts[word]++;
                else
                    counts[word] = 1;

            }

            return counts;
        }
    }
}
