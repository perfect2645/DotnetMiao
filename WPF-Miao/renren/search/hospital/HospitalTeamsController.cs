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

namespace renren.search.hospital
{
    internal class HospitalTeamsController : HttpClientBase
    {
        public HospitalTeamsController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task GetHospitalTeamsAsync()
        {
            try
            {
                await Task.Factory.StartNew(() => GetHospitalTeams());
                await Task.Factory.StartNew(() => GetTeamDetail());
            }
            catch (Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
            }
        }

        public bool GetHospitalTeams()
        {
            var url = "https://www.medic.ren/PM-server/mobteam/getHospitalTeams";

            var content = new HospitalTeamsContent(url);

            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetHospitalTeams - response == null");
                    return false;
                }

                var result = response.JsonBody.RootElement;
                AnalysisResult(result);

                return true;
            }
            catch (HttpException ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetHospitalTeams失败 - {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetHospitalTeams失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        public bool GetTeamDetail()
        {
            var url = "https://www.medic.ren/PM-server/mobteam/getTeamDetail";

            var content = new TeamDetailContent(url);

            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetTeamDetail - response == null");
                    return false;
                }

                var result = response.JsonBody.RootElement;
                AnalysisTeamDetail(result);

                return true;
            }
            catch (HttpException ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetTeamDetail失败 - {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetTeamDetail失败 - {ex.Message} - {ex.StackTrace}");
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
            MainSession.PrintLogEvent.Publish(this, data, $"保存Team Detail");
        }
    }
}
