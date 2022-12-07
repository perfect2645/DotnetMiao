using gaoxin.common;
using gaoxin.session;
using Utils;

namespace gaoxin.search
{
    internal class UserContent : GaoxinContent
    {
        private const string url = "https://ymglfw.care4u.cn/npApii/vaccineDisPark/selectInfo";
        public UserContent(GaoxinLogin loginInfo) : base(url, "获取user", loginInfo)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("disparkId", MainSession.DisparkId);
        }
    }
}
