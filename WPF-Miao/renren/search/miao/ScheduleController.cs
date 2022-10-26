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

namespace renren.search.miao
{
    internal class ScheduleController : HttpClientBase
    {
        public ScheduleController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task GetServiceScheduleAsync()
        {
            try
            {
                await Task.Factory.StartNew(() => GetServiceSchedule());
                await Task.Factory.StartNew(() => GetScheduleDetail());
            }
            catch (Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
            }
        }

        public bool GetServiceSchedule()
        {
            var url = "https://www.medic.ren/PM-server/mobteam/getServiceSchedule";

            var content = new ScheduleContent(url);

            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetServiceSchedule - response == null");
                    return false;
                }

                var result = response.JsonBody.RootElement;
                AnalysisResult(result);

                return true;
            }
            catch (HttpException ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetServiceSchedule失败 - {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetServiceSchedule失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool GetScheduleDetail()
        {
            var url = "https://www.medic.ren/PM-server/mobserviceTimeDef/getServiceScheduleDetail";

            var content = new ScheduleDetailContent(url);

            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetScheduleDetail - response == null");
                    return false;
                }

                var result = response.JsonBody.RootElement;
                AnalysisTeamDetail(result);

                return true;
            }
            catch (HttpException ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetScheduleDetail失败 - {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetScheduleDetail失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private void AnalysisResult(JsonElement jsonElement)
        {
            var dicResult = JsonAnalysis.JsonToDic(jsonElement);
            var code = dicResult["code"].ToInt();
            var message = dicResult["message"].NotNullString();

            var dataJsonElement = jsonElement.GetProperty("data").GetProperty("list");
            var dataList = JsonAnalysis.JsonToDicList(dataJsonElement);

            var nineJiaTeamId = MainSession.PlatformSesstion[Constants.TeamId].NotNullString();
            var data = dataList?.FirstOrDefault(x => x.ContainsValue(nineJiaTeamId));
            if (code != 200 || !data.HasItem())
            {
                throw new HttpException($"code = {code}, message = {message}, data.count = {dataList?.Count}");
            }

            data.AddOrUpdate(Constants.TeamId, data[Constants.Id]);
            MainSession.AddHospitalSession(data);
        }

        private void AnalysisTeamDetail(JsonElement jsonElement)
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
            MainSession.HospitalSession.AddOrUpdate(data);
            //MainSession.PrintLogEvent.Publish(this, data, $"保存GetScheduleDetail");

            MainSession.PrintLogEvent.Publish(this, "查到苗");
        }
    }
}
