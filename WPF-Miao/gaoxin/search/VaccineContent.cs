using gaoxin.common;
using gaoxin.session;

namespace gaoxin.search
{
    internal class VaccineContent : GaoxinContent
    {
        private const string url = "https://ymglfw.care4u.cn/npApii/slVaccineDispark/selectVaccineInfo";
        public UserInfo UserInfo { get; set; }
        public VaccineContent(UserInfo userInfo) : base(url, "查苗", null)
        {
            UserInfo = userInfo;
        }
        protected override void BuildHeader()
        {
            base.BuildHeader();
            AddHeader("token", UserInfo.Token);
        }

        private void BuildContent()
        {
            AddContent("disparkId", MainSession.DisparkId);
            AddContent("yhid", UserInfo.daid);
        }
    }
}
