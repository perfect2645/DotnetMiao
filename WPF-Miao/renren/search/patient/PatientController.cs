using HttpProcessor.Client;
using HttpProcessor.Content;
using renren.session;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils.json;

namespace renren.search.patient
{
    internal class PatientController : HttpClientBase
    {
        public PatientController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserInfoAsync()
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    var isUserGet = GetUser();
                    if (!isUserGet)
                    {
                        return;
                    }
                    GetPatient();
                });
            }
            catch(Exception ex)
            {
                //innerex
            }
        }

        public bool GetUser()
        {
            var url = "https://www.medic.ren/PM-server/mobuserHospital/getUser";

            var content = new SearchContent(url);

            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - response == null");
                    return false;
                }

                var result = response.JsonBody.RootElement.GetProperty("doccustom");
                if (result.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser失败");
                    return false;
                }
                AnalizeResult(result);

                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetUser失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        public bool GetPatient()
        {
            var url = "https://www.medic.ren/PM-server/mobpatient/getPatients";

            var content = new PatientContent(url);

            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetPatient - response == null");
                    return false;
                }

                var result = response.JsonBody.RootElement.GetProperty("doccustom");
                if (result.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                    return false;
                }
                AnalizeResult(result);

                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private void AnalizeResult(JsonElement jsonElement)
        {
            var dicResult = JsonAnalysis.JsonToDic(jsonElement);

            //MainSession.MiaoSession.AddOrUpdate(dicResult);
            MainSession.PrintLogEvent.Publish(this, dicResult, $"保存用户信息");
        }
    }
}
