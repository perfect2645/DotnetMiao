using Sxjk.login;
using Sxjk.session;
using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using System.Collections.Generic;
using Sxjk.common;
using Utils.stringBuilder;

namespace Sxjk.appointment
{
    public class YuyueController : SxjkController
    {
        public bool IsSuccess { get; set; }
        private bool IsHeaderBuilt { get; set; }
        public YuyueController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool YuyueAsync(Order order)
        {
            if (IsSuccess)
            {
                return IsSuccess;
            }

            MainSession.PrintLogEvent.Publish(null, $"开始预约：{order.ToLogString()}");
            var content = new YuyueContent(order, order.User);
            return Yuyue(content);
        }

        internal bool Yuyue(YuyueContent content)
        {
            try
            {
                if (IsSuccess)
                {
                    return IsSuccess;
                }
                
                if (!IsHeaderBuilt)
                {
                    content.BuildDefaultHeaders(Client);
                    IsHeaderBuilt = true;
                }
                HttpDicResponse response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败 - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var resultDic = CheckResult(root);
                if (!resultDic.HasItem())
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败 - {response?.Message}");
                    return false;
                }

                var success = resultDic.GetString("success");
                if ("False".Equals(success, StringComparison.OrdinalIgnoreCase))
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败 - {resultDic.GetString("message")}");
                    return false;
                }

                var message = resultDic.GetString("message");
                if (message == "预订成功！")
                {
                    MainSession.PrintLogEvent.Publish(this, $"{content.Order.UserName} - 预订成功");

                    return true;
                }

                var data = resultDic.GetString("data");

                return CheckOrder(data, content.Order);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
                return false;
            }
        }

        private bool CheckOrder(string data, Order order)
        {
            var appointData = JsonAnalysis.JsonToDic(data);
            var message = appointData.GetString("message");
            var adultBespeakListStr = appointData.GetString("adultBespeakList");
            var adultBespeakList = adultBespeakListStr.ToObjDicList();


            return true;
        }
    }
}
