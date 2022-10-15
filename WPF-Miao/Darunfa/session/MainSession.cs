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
        }
    }
}
