using HttpProcessor.Client;
using HttpProcessor.Content;
using Jkchegu.appointment;
using Jkchegu.session;
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

namespace Jkchegu.search
{
    internal class DateCountController1 : HttpClientBase
    {
        public IntervalOnTime SearchInterval { get; private set; }

        private readonly object OrderLock = new object();

        public string Date { get; private set; }
        public bool IsGet { get; set; }

        public DateCountController1(HttpClient httpClient) : base(httpClient)
        {

        }

        internal void Init(string date, DateTime? startTime)
        {
            Date = date;
            SearchInterval = new IntervalOnTime(async () => await SearchByDateAsync(date), date, startTime ?? DateTime.Now, 200);
        }

        public async Task SearchByDateAsync(string date)
        {
            await Task.Factory.StartNew(() => SearchBydate(date));
        }

        public string SearchBydate(string date)
        {
            var url = "http://app.whkfqws.com/wx-mobile/Reservations/vaccinavaccina_DateCount.do";

            var etid = JkSession.DefaultEtid;
            var content = new DateCountContent(etid, url);
            var defaultUser = JkSession.DefaultUser;
            content.AddHeader("Cookie", defaultUser.Session);
            content.BuildDefaultHeaders(Client);
            content.AddContent("yyDate", date);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"Search({date}) - {response?.Message}");
                    return response?.Message;
                }

                var result = response.JsonBody.RootElement;
                var hasMiao = response.JsonBody.RootElement.GetProperty("doccustom");
                if (hasMiao.ValueKind == JsonValueKind.Null)
                {
                    var res = $"没查到苗 {date}";
                    Log(res);
                    return res;
                }
                SearchInterval?.StopInterval();
                AnalysisResult(result, date);
                return $"查到苗{date}";
            }
            catch (Exception ex)
            {
                JkSession.PrintLogEvent.Publish(this, $"未查到苗{date} - {ex.Message} - {ex.StackTrace}");
                return $"{ex.Message}";
            }
        }

        private void AnalysisResult(JsonElement jsonElement, string date, string time = "")
        {
            lock (OrderLock)
            {
                if (JkSession.MiaoSession.ContainsKey(date) && JkSession.MiaoSession[date] != null)
                {
                    return;
                }
                lock (OrderLock)
                {
                    SaveMiaoInfo(jsonElement, date, time);
                }
            }
        }

        private void SaveMiaoInfo(JsonElement jsonElement, string date, string time = "")
        {
            var pdList = jsonElement.GetProperty("pdList");
            var dateDic = JsonAnalysis.JsonToDicList(pdList);
            var dateList = dateDic.Select(x => x.Values.FirstOrDefault()).ToList();

            if (!dateList.Contains(date))
            {
                return;
            }

            var doccustom = jsonElement.GetProperty("doccustom");
            var doccustomDic = JsonAnalysis.JsonToDic(doccustom);

            var availableTime = doccustomDic.Where(pair =>
                pair.Key.StartsWith("DATE", StringComparison.OrdinalIgnoreCase)
                && pair.Value.NotNullString() != "0").Select(x => x.Key).ToHashSet();

            JkSession.PrintLogEvent.Publish(this, $"查到苗{date}");

            if (!string.IsNullOrEmpty(time))
            {
                if (availableTime.Contains(time))
                {
                    availableTime = availableTime.Where(s => s == time).ToHashSet();
                    BuildExchangeList(date, doccustomDic, availableTime);
                    return;
                }
            }

            BuildOrderList(date, doccustomDic, availableTime);
        }

        private static void BuildOrderList(string date, Dictionary<string, object> doccustomDic, HashSet<string> availableTime)
        {
            var orderList = new List<Order>();
            foreach(var time in availableTime)
            {
                orderList.Add(new Order(date, time, doccustomDic));
            }

            orderList = orderList.DisorderItems();

            var appointEventArgs = new AppointEventArgs
            {
                OrderList = orderList
            };
            JkSession.AppointEvent.Publish(null, appointEventArgs);
        }

        #region 手动 / 转号

        internal object SearchBydateTime(string date, string time)
        {
            var url = "http://app.whkfqws.com/wx-mobile/Reservations/vaccinavaccina_DateCount.do";

            var content = new DateCountContent(JkSession.DefaultEtid, url);
            content.AddHeader("Cookie", JkSession.DefaultUser.Session);
            content.BuildDefaultHeaders(Client);
            content.AddContent("yyDate", date);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"Search({date}-{time}) - {response?.Message}");
                    return response?.Message;
                }

                var result = response.JsonBody.RootElement;
                var hasMiao = response.JsonBody.RootElement.GetProperty("doccustom");
                if (hasMiao.ValueKind == JsonValueKind.Null)
                {
                    var res = $"没查到苗 {date}";
                    Log(res);
                    return res;
                }
                SearchInterval?.StopInterval();
                AnalysisResult(result, date, time);
                return $"SearchBydateTime end - {date}";
            }
            catch (Exception ex)
            {
                JkSession.PrintLogEvent.Publish(this, $"未查到苗{date} - {ex.Message} - {ex.StackTrace}");
                return $"{ex.Message}";
            }
        }

        private static void BuildExchangeList(string date, Dictionary<string, object> doccustomDic, HashSet<string> availableTime)
        {
            var orderList = new List<Order>();
            foreach (var time in availableTime)
            {
                orderList.Add(new Order(date, time, doccustomDic));
            }

            orderList = orderList.DisorderItems();

            var appointEventArgs = new AppointEventArgs
            {
                OrderList = orderList,
                OrderType = "Exchange"
            };
            JkSession.AppointEvent.Publish(null, appointEventArgs);
        }

        #endregion 手动 / 转号
    }
}
