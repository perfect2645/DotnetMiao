using Zhuzher.Common;
using Zhuzher.search;

namespace Zhuzher.Play
{
    internal class GuessBetContent : OnewoContent
    {
        private const string _url = "https://z.onewo.com/market/api/activity/guess/bet";
        public GuessBet GuessBet { get; set; }

        public GuessBetContent(UserProject user, GuessBet guessBet) : base(_url, user)
        {
            GuessBet = guessBet;
            AddDeviceId();
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("activityId", GuessBet.ActivityId);
            AddContent("activityGuessId", GuessBet.ActivityGuessId);
            AddContent("optionId", GuessBet.OptionId);
            AddContent("projectCode", User.ProjectCode);
        }
    }
}