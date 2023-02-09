using Huangshi.appointment;
using Huangshi.common;
using Huangshi.login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Huangshi.search
{
    internal class DateContent : MainContent
    {
        private static string url = "http://gzh.51kys.cn/hssfybjyjkglzx_web/order/GetSJD";

        public string Date { get; set; }

        public DateContent(HuangshiLogin user, string date) : base(url, user)
        {
            Date = date;
            ContentType = "application/json";
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("tjjgid", "YY201912192043290001");
            AddContent("packageId", "000109");
            AddContent("rq", Date);
        }
    }
}
