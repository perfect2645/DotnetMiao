using Zhuzher.Common;
using Zhuzher.search;

namespace Zhuzher.collectsun
{
    internal class AnnouncementContent : ZhuzherContent
    {
        private const string Url = "https://chaos.4009515151.com/fd/api/announcements?page=1&per_page=10&project_code=";
        public AnnouncementContent(UserProject user) : base(Url, user)
        {
            RequestUrl = $"{Url}{user.ProjectCode}";
        }
    }
}
