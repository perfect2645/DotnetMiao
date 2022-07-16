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

        private static string _cookie;

        public static string Cookie
        {
            get { return _cookie; }
            set
            {
                _cookie = value;
                NotifyStaticChanged.NotifyStatic(() => Cookie);
            }
        }
    }
}
