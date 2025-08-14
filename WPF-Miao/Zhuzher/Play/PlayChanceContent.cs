using Zhuzher.Common;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Play
{
    internal class PlayChanceContent : OnewoContent
    {
        public const string Url = "https://z.onewo.com/market/api/activity/chance/";
        public PlayChanceContent(UserProject user) : base(Url, user)
        {
            BuildUrl();
            BuildHeader();
        }

        private void BuildUrl()
        {
            RequestUrl= $"{Url}{MainSession.ActivityId}";
        }

        private void BuildHeader()
        {
            AddDeviceId();
        }
    }
}
