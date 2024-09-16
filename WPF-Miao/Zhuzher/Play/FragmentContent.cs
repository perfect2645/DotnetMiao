using Zhuzher.Common;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Play
{
    internal class FragmentContent : OnewoContent
    {
        private const string _exchangeUrl = "https://z.onewo.com/market/api/activity/fragment/exchange";
        private const string _lotteryUrl = "https://z.onewo.com/market/api/activity/fragment/lottery";
        private const string _holdUrl = "https://z.onewo.com/market/api/activity/fragment/hold";

        public int ActivityGameId { get; set; }
        public int GoodId { get; set; }

        public FragmentContent(UserProject user, int activityGameId, int goodId) : base(_exchangeUrl, user)
        {
            AddDeviceId();

            ActivityGameId = activityGameId;
            GoodId = goodId;
            BuildExchangeContent();
        }

        public FragmentContent(UserProject user, int activityGameId) : base(_lotteryUrl, user)
        {
            AddDeviceId();

            ActivityGameId = activityGameId;
            BuildLotteryContent();
        }

        public FragmentContent(UserProject user, int activityGameId, string activityType ="hold") : base(_holdUrl, user)
        {
            AddDeviceId();

            ActivityGameId = activityGameId;
            BuildLotteryContent();
        }

        private void BuildExchangeContent()
        {
            AddContent("activityGameId", ActivityGameId);
            AddContent("activityId", MainSession.ActivityId);
            AddContent("goodId", GoodId);
            AddContent("projectCode", User.ProjectCode);
            AddContent("projectName", User.ProjectName);
        }

        private void BuildLotteryContent()
        {
            AddContent("activityGameId", ActivityGameId);
            AddContent("activityId", MainSession.ActivityId);
            AddContent("projectCode", User.ProjectCode);
            AddContent("projectName", User.ProjectName);
        }
    }
}
