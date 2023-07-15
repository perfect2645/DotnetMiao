using Dalian.common;
using Dalian.login;
using Dalian.session;
using System;
using System.Reflection.Emit;
using Utils;

namespace Dalian.appointment
{
    internal class YuyueContent : DalianContent
    {
        private static string baseUrl = "https://hlwyy.dlfeyljt.com/patient/v1/appoint/regPoint";
        private static string path = "/appoint/regPoint";
        public Order Order { get; private set; }
        public YuyueContent(Order order, DalianLogin user) : base(baseUrl, path, user)
        {
            Order = order;
            BuildRequestData();
            BuildHeader();
        }

        protected override void BuildRequestData()
        {
            Parameters.AddOrUpdate("pointId", Order.PointId);
            Parameters.AddOrUpdate("pointName", Order.PointName);
            Parameters.AddOrUpdate("pointDate", Order.PointDate);
            Parameters.AddOrUpdate("regLevelId", Order.RegLevelId);
            Parameters.AddOrUpdate("regLevelName", Order.RegLevelName);
            Parameters.AddOrUpdate("patientId", User.PatientId);
            Parameters.AddOrUpdate("deptId", Order.DeptId);
            Parameters.AddOrUpdate("drId", Order.DrId);
            Parameters.AddOrUpdate("visitTime", Order.VisitTime);
            Parameters.AddOrUpdate("medInsType", string.Empty);
            Parameters.AddOrUpdate("specialDeptType", string.Empty);
            Parameters.AddOrUpdate("beginTime", Order.BeginTime);
            Parameters.AddOrUpdate("endTime", Order.EndTime);
            Parameters.AddOrUpdate("diagnoseFee", Order.DiagnoseFee);
            Parameters.AddOrUpdate("noonId", Order.NoonId);
            Parameters.AddOrUpdate("noonName", Order.NoonName);
            Parameters.AddOrUpdate("regFee", string.Empty);
            Parameters.AddOrUpdate("inspectFee", string.Empty);
            Parameters.AddOrUpdate("hisDeptId", string.Empty);
            Parameters.AddOrUpdate("referenceCode", string.Empty);
            base.BuildRequestData();
        }
    }
}
