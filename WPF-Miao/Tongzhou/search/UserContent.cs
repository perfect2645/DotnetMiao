using Tongzhou.common;
using Tongzhou.login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tongzhou.search
{
    internal class UserContent : TongzhouContent
    {
        private static string baseUrl = "https://fwcs.linkingcloud.cn/card/GetBindLst";
        public UserContent(TongzhouLogin user) : base(baseUrl, user)
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
