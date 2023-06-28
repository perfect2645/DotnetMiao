using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace Sxjk.common
{
    public class SxjkController : HttpClientBase
    {
        public SxjkController(HttpClient httpClient) : base(httpClient)
        {
        }

        protected Dictionary<string, object> CheckResult(JsonElement root)
        {
            var resResult = new Dictionary<string, object>();
            var result = root.GetProperty("result").GetString();
            if (string.IsNullOrEmpty(result))
            {
                return resResult;
            }

            var resDecode = Encrypt.DecryptAes(result);
            if (string.IsNullOrEmpty(resDecode))
            {
                return resResult;
            }

            resDecode = resDecode.Replace("\0", string.Empty);
            resResult = JsonAnalysis.JsonToDic(resDecode);

            var sign = root.GetProperty("sign").GetString();
            resResult.AddOrUpdate("sign", sign);

            return resResult;
        }
    }
}
