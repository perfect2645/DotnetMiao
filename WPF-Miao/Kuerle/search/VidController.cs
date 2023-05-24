using HttpProcessor.Client;
using Kuerle.session;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Utils;
using Utils.json;

namespace Kuerle.search
{
    public class VidController : HttpClientBase
    {
        public VidController(HttpClient httpClient) : base(httpClient)
        {
        }

        public string GetVid(string pid)
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new VidContent(defaultUser, pid);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetVid - {response?.Message},请检查参数");
                    return string.Empty;
                }
                var root = response.JsonBody.RootElement;
                var success = root.GetProperty("Success").GetBoolean();
                if (!success)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetVid失败 - success = {success}");
                    return string.Empty;
                }

                var data = root.GetProperty("Data");

                return SaveVid(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return string.Empty;
            }
        }

        private string SaveVid(JsonElement data)
        {
            var vidList = JsonAnalysis.JsonToDicList(data);
            if (!vidList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取Vid失败, vid 是空的");
                return string.Empty;
            }

            var defaultVidInfo = vidList.FirstOrDefault();

            var vid = defaultVidInfo.GetString("VID");

            MainSession.PrintLogEvent.Publish(this, $"VID get - {vid}");
            return vid;
        }
    }
}
