using HttpProcessor.Content;
using JkzlSearcher.common;
using JkzlSearcher.session;
using Utils.datetime;

namespace JkzlSearcher.search
{
    internal class HosDeptContent : JkzlContent
    {
        private const string Url = "https://appoint.yihu.com/appoint/do/dept/getHosDeptList";
        public string HospitalId { get; private set; }
        public HosDeptContent() : base(Url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("t", DateTimeUtil.GetTimeStamp());
            AddContent("cooperate", "1");
            AddContent("orderColumns", "orderId%3A0");
            AddContent("channelId", "1000031");
        }

        public void SetHospitalId(string hospitalId)
        {
            HospitalId = hospitalId;
            AddContent("hospitalId", HospitalId);
        }

    }
}
