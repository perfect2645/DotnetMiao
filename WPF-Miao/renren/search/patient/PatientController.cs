using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using renren.session;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace renren.search.patient
{
    internal class PatientController : HttpClientBase
    {
        public PatientController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async void GetUserInfoAsync()
        {
            try
            {
                await Task.Factory.StartNew(() => GetSignature());
                await Task.Factory.StartNew(() =>
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

        private void GetSignature()
        {
            var url = "https://www.medic.ren/PM-server/wechatAuthorizationInfo/getSignature";

            var content = new AuthContent(url);

            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetSignature - response == null");
                    return;
                }

                var result = response.JsonBody.RootElement;
                AnalizeGetSign(result);
            }
            catch (HttpException ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetSignature失败 - {ex.Message}");
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetSignature失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        public bool GetUser()
        {
            var url = "https://www.medic.ren/PM-server/mobuserHospital/getUser";

            var content = new SearchContent(url);

            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - response == null");
                    return false;
                }

                var result = response.JsonBody.RootElement;
                AnalizeGetUser(result);

                return true;
            }
            catch (HttpException ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetUser失败 - {ex.Message}");
                return false;
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
                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetPatient - response == null");
                    return false;
                }

                var result = response.JsonBody.RootElement;
                AnalizeGetPatient(result);

                return true;
            }
            catch (HttpException ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败 - {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private void AnalizeGetSign(JsonElement jsonElement)
        {
            var dicResult = JsonAnalysis.JsonToDic(jsonElement);
            var code = dicResult["code"].ToInt();
            var message = dicResult["message"].NotNullString();

            var dataJsonElement = jsonElement.GetProperty("data");
            var data = JsonAnalysis.JsonToDic(dataJsonElement);

            if (code != 200 || !data.HasItem())
            {
                throw new HttpException($"code = {code}, message = {message}, data.count = {data?.Count}");
            }
            MainSession.PlatformSesstion.AddOrUpdate(data);
            MainSession.PrintLogEvent.Publish(this, dicResult, $"保存App Sign");
        }

        private void AnalizeGetUser(JsonElement jsonElement)
        {
            var dicResult = JsonAnalysis.JsonToDic(jsonElement);
            var code = dicResult["code"].ToInt();
            var message = dicResult["message"].NotNullString();

            var dataJsonElement = jsonElement.GetProperty("data");
            var data = JsonAnalysis.JsonToDic(dataJsonElement);

            if (code != 200 || !data.HasItem())
            {
                throw new HttpException($"code = {code}, message = {message}, data.count = {data?.Count}");
            }
            MainSession.AddUserSession(data);
            MainSession.PrintLogEvent.Publish(this, dicResult, $"保存用户信息");
        }

        private void AnalizeGetPatient(JsonElement jsonElement)
        {
            var dicResult = JsonAnalysis.JsonToDic(jsonElement);
            var code = dicResult["code"].ToInt();
            var message = dicResult["message"].NotNullString();

            var dataJsonElement = jsonElement.GetProperty("data");
            var dataList = JsonAnalysis.JsonToDicList(dataJsonElement);
            var data = dataList?.FirstOrDefault();
            if (code != 200 || !data.HasItem())
            {
                throw new HttpException($"code = {code}, message = {message}, data.count = {dataList?.Count}");
            }
            MainSession.AddUserSession(data);
            MainSession.PrintLogEvent.Publish(this, dicResult, $"保存Patient信息");
        }
    }
}
