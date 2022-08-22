using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Baohe.search.user
{
    internal class MemberListContent : ContentBase
    {
        public MemberListContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        public override void BuildDefaultHeaders(HttpClient httpClient)
        {
            base.BuildDefaultHeaders(httpClient);
        }

        public void BuildContent()
        {
            AddContent(Constant.AccountSn, BaoheSession.UserSession[Constant.AccountSn]);
            AddContent("hosKey", string.Empty);
        }

        public string BuildReferer()
        {
            var platformType = BaoheSession.PlatformSesstion[Constant.PlatformType];
            var hospitalId = BaoheSession.PlatformSesstion[Constant.HospitalId];

            var refererTemplate = $"https://appoint.yihu.com/appoint/doctor/ghDoctorList.html?platformType={platformType}&hospitalId={hospitalId}&exConsult=&consultHosId={hospitalId}";

            return refererTemplate;
        }

        public override string BuildCookie()
        {
            var sb = new StringBuilder();
            var cookie = base.BuildCookie();


            cookie = $"{cookie}{sb?.ToString() ?? string.Empty}";
            return cookie;
        }
    }
}
