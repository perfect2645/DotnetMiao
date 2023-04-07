using HosFour.common;
using HosFour.login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HosFour.search
{
    internal class UserContent : HosFourContent
    {
        private static string baseUrl = "https://fwcs.linkingcloud.cn/card/GetBindLst";
        public UserContent(HosFourLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("dataSource", "yuyue");
            AddContent("cardTag", string.Empty);
            AddContent("t", 1);
        }
    }
}
