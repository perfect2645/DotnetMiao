using HttpProcessor.Client;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
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
                    DianSession.HospitalSession.AddOrUpdate(doctorId, contentDic);
                }
            }
        }

        #endregion Search
    }
}
