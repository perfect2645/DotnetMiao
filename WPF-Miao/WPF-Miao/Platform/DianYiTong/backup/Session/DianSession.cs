using System.Collections.Generic;
using WPF_Miao.Platform.DianYiTong.Order;

namespace WPF_Miao.Platform.DianYiTong.Session
{
    internal static class DianSession
    {
        static DianSession()
        {
            HospitalSession = new Dictionary<string, object>();
        }
        public static OrderRequest OrderRequest { get; set; }

        #region Required Info

        public static Dictionary<string, object> HospitalSession { get; set; }


        #endregion Required Info
    }
}
