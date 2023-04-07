using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Container;
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
using Zhi.appointment;
using Zhi.session;

namespace Zhi.search
{
    public class DoctorController : HttpClientBase
    {
        public bool IsDoctorGet { get; set; }
        public string Date { get; private set; }
        internal DoctorContent DoctorContent { get; private set; }
        public IntervalOnTime SearchInterval;

        private readonly object _lock = new object();

        public DoctorController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void BuildContent(string date)
        {
            Date = date;

            DoctorContent = new DoctorContent(date);
            DoctorContent.BuildDefaultHeaders(Client);

            SearchInterval = new IntervalOnTime(SearchDoctorAsync, $"{Date}", 300);
        }

        public async void SearchDoctorAsync()
        {
            if (IsDoctorGet)
            {
                SearchInterval.StopInterval();
            }
            await Task.Factory.StartNew(() => SearchDoctor());
        }

        private bool SearchDoctor()
        {

            try
            {
                HttpDicResponse response = PostStringAsync(DoctorContent, HttpProcessor.Content.ContentType.String).Result;
                if (IsDoctorGet)
                {
                    SearchInterval.StopInterval();
                    return true;
                }

                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"获取医生失败{response?.Message}");
                    return false;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                if (!"1".Equals(code))
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取医生失败:code={code}");
                    return false;
                }
                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取医生失败:code={code}");
                    return false;
                }
                return SaveDoctorInfo(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取医生失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool SaveDoctorInfo(JsonElement data)
        {
            var doctorList = JsonAnalysis.JsonToDicList(data);
            if (!doctorList.HasItem())
            {
                Log($"没查到医生信息");
                return false; ;
            }


            lock(_lock)
            {
                if (!IsDoctorGet)
                {
                    lock (_lock)
                    {
                        BuildSearchMiaoController(doctorList);
                    }
                }
            }

            IsDoctorGet = true;

            return true;
        }

        private void BuildSearchMiaoController(List<Dictionary<string, object>> doctorList)
        {
            IsDoctorGet = true;

            MainSession.PlatformSession.AddOrUpdate(Date, doctorList);

            foreach(var doctor in doctorList)
            {
                var docCode = doctor.GetString("doctorCode");
                var docName = doctor.GetString("doctorName");
                if (string.IsNullOrEmpty(docName))
                {
                    docName = "普通号(上午)";
                }

                var timeFlag = "1";
                if (docName.Contains("下午"))
                {
                    timeFlag = "2";
                }

                var docType = doctor.GetString("doctorType");
                var regFee = doctor.GetString("regFee");
                MainSession.PlatformSession.AddOrUpdate("regFee", regFee);

                var searchMiaoController = MainSession.SearchSession.GetController($"{docCode}|{timeFlag}");
                if (searchMiaoController == null)
                {
                    searchMiaoController = HttpServiceController.GetService<SearchMiaoController>();
                }
                searchMiaoController.BuildContent(Date, timeFlag, docCode, docType);
                searchMiaoController.SearchInterval.StartIntervalOntime();
            }
        }
    }
}
