using Huangshi.appointment;
using Huangshi.common;
using Huangshi.login;
using Huangshi.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Huangshi.search
{
    internal class TimeContent : MainContent
    {
        private static string url = "http://gzh.51kys.cn/hssfybjyjkglzx_web/order/GetSJD";

        public string Date { get; set; }

        public TimeContent(HuangshiLogin user, string date) : base(url, user)
        {
            Date = date;
            ContentType = "application/json";
            BuildContent();
        }

        private void BuildContent()
        {
            var packageId = MainSession.PlatformSession.GetString(Constants.PackageId);
            AddContent("tjjgid", "YY201912192043290001");
            AddContent("packageId", packageId);
            AddContent("rq", Date);
        }
    }
}
