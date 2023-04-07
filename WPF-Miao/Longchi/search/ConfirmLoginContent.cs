using Longchi.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longchi.search
{
    internal class ConfirmLoginContent : LongchiContent
    {

        private static string url = "http://hpv_ym.zzytrj.net:15003/api/yuyue.php";
        public ConfirmLoginContent(string cookie) : base(url, cookie)
        {
            ContentType= "application/x-www-form-urlencoded";
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("cmd", "yuyue_get_login");
        }
    }
}
