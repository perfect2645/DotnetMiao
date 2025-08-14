using Huaxi.common;
using Huaxi.login;
using Huaxi.session;
using System.Text;
using Utils;

namespace Huaxi.Yzm
{
    internal class YzmContent : HuaxiContent
    {
        private static string baseUrl = "https://mcpwxp.motherchildren.com/cloud/ppclient/msg/getImageCode";
        public YzmContent(HuaxiLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        protected override void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            AddContent("organCode", hosId);
            AddContent("appCode", Constants.AppCode);
            AddContent("channelCode", Constants.ChannelCode);
            AddContent("type", "WECHAT");
        }
    }
}
