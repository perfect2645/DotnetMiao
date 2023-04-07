using Base.viewmodel.status;
using HttpProcessor.Client;
using Ych.appointment;
using Ych.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.number;
using Utils.stringBuilder;
using Utils.timerUtil;
using Utils.datetime;

namespace Ych.search
{
    public class SearchMiaoController : HttpClientBase
    {
        public bool IsMiaoGet { get; set; }
        public string Date { get; private set; }
        internal SearchMiaoContent SearchMiaoContent { get; private set; }
        public IntervalOnTime SearchInterval;

        public SearchMiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void BuildContent(string date, string timeFlag, string doctorCode, string doctorType)
        {
            Date = date;

            SearchMiaoContent = new SearchMiaoContent(date, timeFlag, doctorCode, doctorType);
            SearchMiaoContent.BuildDefaultHeaders(Client);

            SearchInterval = new IntervalOnTime(SearchMiaoAsync, $"{Date}", 300);
        }

        public async void SearchMiaoAsync()
        {
            if (IsMiaoGet)
            {
                SearchInterval.StopInterval();
            }
            await Task.Factory.StartNew(() => SearchMiao());
        }

        private bool SearchMiao()
        {

            try
            {
                HttpDicResponse response = PostStringAsync(SearchMiaoContent, HttpProcessor.Content.ContentType.String).Result;
                if (IsMiaoGet)
                {
                    SearchInterval.StopInterval();
                    return true;
                }

                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"获取miao失败{response?.Message}");
                    return false;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                if (!"1".Equals(code))
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取miao失败:code={code}");
                    return false;
                }
                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取miao失败:code={code}");
                    return false;
                }
                return SaveMiaoInfo(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool SaveMiaoInfo(JsonElement miaoElement)
        {
            var scheduleList = JsonAnalysis.JsonToDicList(miaoElement);
            if (scheduleList == null)
            {
                Log($"没开始放苗data is empty");
                return false; ;
            }

            var avaliableScheduleList = scheduleList.Where(x => x["leftNumber"].NotNullString().ToInt() > 0).ToList();
            if (!avaliableScheduleList.HasItem())
            {
                Log($"查到{scheduleList.Count}个苗,但是没有可用苗");
                return false;
            }

            IsMiaoGet = true;

            BuildOrderList(avaliableScheduleList);

            MainSession.SetStatus(MiaoProgress.MiaoGet);
            return true;
        }

        private void BuildOrderList(List<Dictionary<string, object>> scheduleList)
        {
            var baseOrderList = new List<Order>();

            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var deptName = MainSession.PlatformSession.GetString(Constants.DeptName);
            var userName = MainSession.PlatformSession.GetString(Constants.UserName);
            var userId = MainSession.PlatformSession.GetString(Constants.UserId);
            var userInfo = MainSession.PlatformSession["user"] as Dictionary<string, object>;

            foreach (var schedule in scheduleList)
            {
                var startTime = DateTimeUtil.GetDateTime(schedule.GetString("beginTime"), "HH-mm");
                var endTime = DateTimeUtil.GetDateTime(schedule.GetString("endTime"), "HH-mm");
                var timeRange = $"{startTime}-{endTime}";
                baseOrderList.Add(new Order
                {
                    BeginTime = startTime,
                    BeginTimeEncode = YchEncode(startTime),
                    EndTime = endTime,
                    EndTimeEncode = YchEncode(endTime),
                    DepartmentCode = deptId,
                    DepartmentName = deptName,
                    DoctorCode = SearchMiaoContent.DoctorId,
                    PatientCode = userId,
                    PatientIdentityCardNumber = userInfo.GetString("idcard"),
                    PatientMobile = userInfo.GetString("mobile"),
                    PatientName = userName,
                    PatientNameEncode = YchEncode(userName),
                    RegFee = MainSession.PlatformSession.GetString("regFee"),
                    ScheduleDate = Date,
                    ScheduleDetailId = schedule.GetString("serialNumber"),
                    TimeFlag = SearchMiaoContent.TimeFlag
                });
            }
            baseOrderList = baseOrderList.DisorderItems();

            var appointEventArgs = new OrderEventArgs
            {
                OrderList = baseOrderList
            };
            MainSession.OrderEvent.Publish(null, appointEventArgs);
        }


        private string YchEncode(string str)
        {
            var encodeStr = UnicodeConverter.Encode(str, true);

            var ychEncode = encodeStr.Replace("-", "%3A");
            ychEncode = ychEncode.Replace("%", "%25");

            return ychEncode;
        }
    }
}
