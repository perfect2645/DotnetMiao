using HttpProcessor.Content;
using Utils.datetime;
using Utils.stringBuilder;
using Zhuzher.Common;
using Zhuzher.session;

namespace Zhuzher.Exchange
{
    internal class ExchangeContent : OnewoContent
    {
        public ExchangeContent(string url, search.UserProject user) : base(url, user)
        {
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddDeviceId();
        }

        private void BuildContent()
        {
            AddContent("activityId", MainSession.ActivityId.ToInt());
        }
    }
}
