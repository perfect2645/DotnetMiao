using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Content;
using Redhouse.appointment;
using Redhouse.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Redhouse.search
{
    internal class MiaoController : HttpClientBase
    {
        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchMiaoAsync()
        {
            Task.Factory.StartNew(() => SearchMiao());
        }

        private void SearchMiao()
        {
            var content = new MiaoContent();
            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"获取Miao失败{response?.Message}");
                    return;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var message = root.GetProperty("message").NotNullString();
                if (!"200".Equals(code))
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取Miao失败:code={code}, message={message}");
                    return;
                }
                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取Miao失败:code={code}, message={message}");
                    return;
                }
                AnalysisResult(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void AnalysisResult(JsonElement jsonElement)
        {
            var dataList = JsonAnalysis.JsonToDicList(jsonElement);
            if (!dataList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗");
                return;
            }

            var userInfo = MainSession.PlatformSession["user"] as Dictionary<string, object>;
            var wechat_CardInfos = userInfo["wechat_CardInfos"] as List<Dictionary<string, object>>;
            var userWechat = wechat_CardInfos.FirstOrDefault();
            var hosId = MainSession.PlatformSession.GetString(Constants.HosId);

            var orderList = new List<Order>();
            foreach (var data in dataList)
            {
                var order = new Order()
                {
                    Address = data.GetString("address"),
                    CardId = userWechat.GetString("id"),
                    CardKind = userWechat.GetString("cardType").ToInt(),
                    CardNo = userWechat.GetString("cardNum"),
                    IdCardNo = userInfo.GetString("certificationNo"),
                    Email = userInfo.GetString("email"),
                    HosId = hosId,
                    IdCardKind = 1,
                    ItemId = data.GetString("itemId"),
                    Mobile = userInfo.GetString("phone"),
                    PatientBirthDate = userInfo.GetString("brithday").Substring(0, 10),
                    PatientInfoId = userInfo.GetString("id"),
                    PatientMarryFlag = 0,
                    PatientName = userInfo.GetString("name"),
                    PatientSexFlag = userInfo.GetString("sex").ToInt(),
                    Phone = userInfo.GetString("phone"),
                    PlanId = data.GetString("planId"),
                    Telephone = userInfo.GetString("phone"),
                };

                orderList.Add(order);
            }

            MainSession.PrintLogEvent.Publish(this, $"查到苗");
            MainSession.SetStatus(MiaoProgress.MiaoGet);
        }
    }
}
