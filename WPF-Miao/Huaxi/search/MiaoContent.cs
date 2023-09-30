using Huaxi.common;
using Huaxi.login;
using Huaxi.session;
using Utils;
using Utils.datetime;

namespace Huaxi.search
{
    internal class MiaoContent : HuaxiContent
    {
        private static string baseUrl = "https://mcpwxp.motherchildren.com/cloud/appointment/publicClient/selDoctorSchedule";

        public MiaoContent(HuaxiLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        protected override void BuildContent()
        {
            var docId = MainSession.PlatformSession.GetString(Constants.DoctorId);

            AddContent("organCode", "platform");
            AddContent("appCode", Constants.AppCode);
            AddContent("channelCode", Constants.ChannelCode);
            AddContent("doctorId", docId);
        }
    }
}
