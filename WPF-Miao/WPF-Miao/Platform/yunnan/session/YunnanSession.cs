using System.Collections.Generic;
using Utils;
using WPF_Miao.Platform.yunnan.viewModel;

namespace WPF_Miao.Platform.yunnan.session
{
    public class YunnanSession
    {
        private YunnanSession() { }

        public static Dictionary<string, SessionItem> MiaoSession { get; set; }

        static YunnanSession()
        {
            Reset();
        }

        public static void AddOrUpdate(SessionItem item)
        {
            MiaoSession.AddOrUpdate(item.UserName, item);
        }

        public static void Reset()
        {
            MiaoSession = new Dictionary<string, SessionItem>();
        }

        public static SessionItem GetSessionItem(string? userName = null)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return MiaoSession["userName"];
            }

            return MiaoSession[userName];
        }
    }
}
