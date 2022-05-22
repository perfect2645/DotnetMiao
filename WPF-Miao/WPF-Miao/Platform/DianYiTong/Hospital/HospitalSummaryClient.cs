using HttpProcessor.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using WPF_Miao.Platform.DianYiTong.Session;

namespace WPF_Miao.Platform.DianYiTong.Hospital
{
    internal class HospitalSummaryClient : HttpClientBase
    {
        public HospitalSummaryClient(HttpClient client) : base(client)
        {

        }

        #region Search

        public void Search(HttpClientContentBase content)
        {
            Search(content, PostSearch);
        }

        private void PostSearch(HttpDicResponse dicResponse)
        {
            var bodyRoot = dicResponse.JsonBody.RootElement;
            var data = bodyRoot.GetProperty("data");

            if (data.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement prop in data.EnumerateArray())
                {
                    var jsonStr = prop.ToString();
                    var contentDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);
                    if (contentDic == null)
                    {
                        continue;
                    }
                    var doctorId = prop.GetProperty("doc_id").GetInt32().ToString();
                    DianSession.SessionData.AddOrUpdate(doctorId, contentDic);
                }
            }
        }

        #endregion Search
    }
}
