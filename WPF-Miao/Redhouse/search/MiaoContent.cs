using Redhouse.common;
using Redhouse.session;
using System;
using System.Collections.Generic;
using Utils;
using Utils.datetime;

namespace Redhouse.search
{
    internal class MiaoContent : RedhouseContent
    {
        private const string url = "https://wxgzh.fckyy.org.cn/api/hosservice/Appointment/GetPlanItemByDepartIds";
        public MiaoContent() : base(url)
        {
            InitContent();
        }

        private void InitContent()
        {
            var data = new Dictionary<string, object>();
            data.AddOrUpdate(MainSession.PlatformSession, Constants.HosId);
            data.AddOrUpdate(MainSession.PlatformSession, Constants.DepartId);

            Contents.AddOrUpdate("data", data);
            Contents.AddOrUpdate("timestamp", DateTimeUtil.GetTimeStamp());
        }
    }
}
