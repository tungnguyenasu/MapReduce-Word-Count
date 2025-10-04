using System.Collections.Generic;
using System.Web.Http;

namespace ReduceService.Controllers
{
    public class ReduceController : ApiController
    {
        [HttpPost]
        [Route("reduce")]
        public Dictionary<string, int> Reduce([FromBody] List<Dictionary<string, int>> inputs)
        {
            var result = new Dictionary<string, int>(System.StringComparer.OrdinalIgnoreCase);

            foreach (var map in inputs)
            {
                foreach (var kv in map)
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
