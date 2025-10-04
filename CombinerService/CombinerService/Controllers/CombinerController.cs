using System.Collections.Generic;
using System.Web.Http;

namespace CombinerService.Controllers
{
    public class CombinerController : ApiController
    {
        [HttpPost]
        [Route("combine")]
        public Dictionary<string, int> Combine([FromBody] List<Dictionary<string, int>> inputs)
        {
            var result = new Dictionary<string, int>(System.StringComparer.OrdinalIgnoreCase);

            foreach (var reduce in inputs)
            {
                foreach (var kv in reduce)
                {
                    if (result.ContainsKey(kv.Key))
                        result[kv.Key] += kv.Value;
                    else
                        result[kv.Key] = kv.Value;
                }
            }

            return result;
        }
    }
}
