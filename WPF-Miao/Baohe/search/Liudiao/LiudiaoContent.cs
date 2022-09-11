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
using Utils.datetime;

namespace Baohe.search.Liudiao
{
    internal class LiudiaoContent : ContentBase
    {
        public ISessionItem SessionItem { get; }

        public LiudiaoContent(string url, ISessionItem sessionItem) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            SessionItem = sessionItem;
            BuildContent();
        }

        public override void BuildDefaultHeaders(HttpClient httpClient)
        {
            base.BuildDefaultHeaders(httpClient);
        }

        private void BuildContent()
        {
            var member = SessionBuilder.GetDefaultMember(SessionItem);
            AddContent("userName", member["Cname"]);
            AddContent("phone", member["Phone"]);
            AddContent("cardNum", SessionItem.SessionDic["cardNumber"]);
            AddContent("cardType", "1");
            AddContent("question1", "2");
            AddContent("question2", "2");
            AddContent("question3", "2");
            AddContent("question4", "2");
            AddContent("question5", "2");
            AddContent("question6", "2");
            AddContent("question7", "2");
            AddContent("question8", "2");
            AddContent("channelId", "2");
            AddContent("sceneId", "");
            AddContent("isGuahao", "");
            AddContent("fmId", "hfssqngzwsy");
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
