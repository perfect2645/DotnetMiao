using Dongxihu.common;
using Dongxihu.login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dongxihu.search
{
    internal class UserContent : DongxihuContent
    {
        private static string baseUrl = "https://fwcs.linkingcloud.cn/card/GetBindLst";
        public UserContent(DongxihuLogin user) : base(baseUrl, user)
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
