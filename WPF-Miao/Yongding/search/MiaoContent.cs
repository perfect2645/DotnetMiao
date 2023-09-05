using Utils;
using Yongding.appointment;
using Yongding.common;
using Yongding.login;
using Yongding.session;

namespace Yongding.search
{
    internal class MiaoContent : YongdingContent
    {
        private static string baseUrl = "http://yiliao2.lefeiniu.com:8081/resource/get_time?id=28&riqi=2023-08-03";

        public MiaoContent(Order order) : base(baseUrl, order.User)
        {
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);

            RequestUrl = $"{baseUrl}id={order.Timeid}&riqi={order.Riqi}";
        }
    }
}
