using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Utils;

namespace WPF_Miao.Platform.yunnan.session
{
    internal class AppSession
    {
        private static int _localTimeOffset = 0;
        public static int LocalTimeOffset
        {
            get
            {
                return ++_localTimeOffset;
            }
        }

        public static string YunnanUrl = "https://weixin.ngarihealth.com/weixin/wx/mp/wxf119c4ff0a602d44/gateway";
        public static string Cookie { get; set; }

    }
}
