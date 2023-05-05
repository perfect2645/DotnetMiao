using HosTwo.common;
using HosTwo.login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HosTwo.search
{
    internal class UserContent : HosTwoContent
    {
        private static string baseUrl = "https://fwcs.linkingcloud.cn/card/GetBindLst";
        public UserContent(HosTwoLogin user) : base(baseUrl, user)
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
