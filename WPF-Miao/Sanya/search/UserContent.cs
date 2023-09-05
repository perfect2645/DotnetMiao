using Sanya.common;
using Sanya.login;
using Sanya.session;
using Utils;

namespace Sanya.search
{
    internal class UserContent : SanyaContent
    {
        private static string baseUrl = "https://public.health.zoenet.cn/medical/medical-card-manage/decoupling/selectPatientCardListByGroup";
        public UserContent(SanyaLogin user) : base(baseUrl, user)
        {
            BuildContent();
            BuildHeaderSign();
        }

        private void BuildContent()
        {
            var hosCode = MainSession.PlatformSession.GetString(Constants.HospitalId);
            AddContent("hosCode", JsReader.EncodeStr(hosCode));
            AddContent("openId", JsReader.EncodeStr(User.OpenId));
            AddContent("platformSingleHospital", JsReader.EncodeStr("0"));
            AddContent("validFlag", JsReader.EncodeStr("1"));
            AddContent("icCardTypeCode", JsReader.EncodeStr("7"));
            AddContent("encryptFlag", 1);
        }

        protected override void BuildHeaderSign()
        {
            base.BuildHeaderSign();
        }
    }
}
