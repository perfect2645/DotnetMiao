using Base.Events;
using Base.session;
using System;
using System.Collections.Generic;
using Utils;

namespace Darunfa.session
{
    internal class MainSession : ISessionContainer
    {
        public static string Cookie { get; set; }
        public static Dictionary<string, object> UserSession { get; set; }
        public static Dictionary<string, object> ShopSession { get; set; }
        public static LogEvents PrintLogEvent { get; set; }


        static MainSession()
        {
            UserSession = new Dictionary<string, object>();
            ShopSession = new Dictionary<string, object>();

            AddDefaultData();
        }

        private static void AddDefaultData()
        {
            UserSession.AddOrUpdate(Constants.ClientId, "a7ea53059fc868e2e3e2dd7c04027035");
            UserSession.AddOrUpdate(Constants.DeviceId, "z3tGgpTr98k8dQdVg6xeeSpiAMOQEPtRuq3x");

            ShopSession.AddOrUpdate(Constants.StoreId, "3052");
            ShopSession.AddOrUpdate(Constants.AddrId, "3A500128-F309-42D9-8A35-00C6E3A0D9E4");
            ShopSession.AddOrUpdate(Constants.PackageName, "泉水店-半日达");

            var packageId = $"{ShopSession[Constants.StoreId]}+day";
            ShopSession.AddOrUpdate(Constants.PackageId, packageId);
        }

        #region Build Date

        public static string BuildDeliveryDay()
        {
            var deliverDay = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
            ShopSession.AddOrUpdate(Constants.DeliveryDay, deliverDay);

            return deliverDay;
        }

        public static string BuildDeliveryTime()
        {
            // 10:00-13:00 14:00-17:00 18:00-21:00

            return "18:00-21:00";
        }

        #endregion Build Date
    }
}
