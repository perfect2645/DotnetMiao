using Dalian.common;
using Dalian.login;
using Dalian.session;
using Utils;

namespace Dalian.search
{
    internal class UserContent : DalianContent
    {
        private static string baseUrl = "https://hlwyy.dlfeyljt.com/patient/v1/myself/queryPatients";
        private static string path = "/myself/queryPatients";

        protected override void BuildRequestData()
        {
            base.BuildRequestData();
        }

        public UserContent(DalianLogin user) : base(baseUrl, path, user)
        {
            BuildRequestData();
            BuildHeader();
        }

        protected override void BuildHeader()
        {
            base.BuildHeader();
        }
    }
}
