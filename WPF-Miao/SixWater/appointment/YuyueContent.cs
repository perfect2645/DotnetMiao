using SixWater.common;
using SixWater.login;
using SixWater.session;
using System;
using System.Collections.Generic;
using Utils;

namespace SixWater.appointment
{
    internal class YuyueContent : SixWaterContent
    {
        private static string url = "https://oss.zsqrmyy.com:8443/patient/register/preRegistration";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.User)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("deptId", Order.DeptId);
            AddContent("doctorId", Order.DoctorId);
            AddContent("doctorScheduleId", Order.DoctorScheduleId);
            AddContent("scheduleDate", Order.ScheduleDate);
            AddContent("orgId", Order.OrgId);


            AddContent("orderNumber", Order.OrderNumber);
            AddContent("registerTypeId", Order.RegisterTypeId);
            AddContent("doctorScheduleId", Order.DoctorScheduleId);
            AddContent("totalFee", Order.TotalFee);
            AddContent("beginTime", Order.BeginTime);
            AddContent("endTime", Order.EndTime);
            AddContent("emergency", Order.Emergency);
            AddContent("registerCategory", Order.RegisterCategory);
            AddContent("familyMemberId", Order.FamilyMemberId);
            AddContent("jsonContent", BuildJsonContent());
            AddContent("attachInfo", new object());
            AddContent("insuranceType", string.Empty);
            AddContent("payMethod", Order.PayMethod);
        }

        private string BuildJsonContent()
        {
            var contentDic = new Dictionary<string, object>();
            var orgName = MainSession.PlatformSession.GetString(Constants.HospitalName);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var deptName = MainSession.PlatformSession.GetString(Constants.DeptName);
            var doctorName = MainSession.PlatformSession.GetString(Constants.DoctorName);

            contentDic.AddOrUpdate("orgId", Order.OrgId);
            contentDic.AddOrUpdate("orgName", orgName);
            contentDic.AddOrUpdate("deptId", deptId);
            contentDic.AddOrUpdate("deptName", deptName);
            contentDic.AddOrUpdate("examinationFeeId", string.Empty);
            contentDic.AddOrUpdate("doctorScheduleId", Order.DoctorScheduleId);
            contentDic.AddOrUpdate("orderNumber", string.Empty);
            contentDic.AddOrUpdate("deptLocation", string.Empty);
            contentDic.AddOrUpdate("patientCardNo", string.Empty);
            contentDic.AddOrUpdate("doctorId", string.Empty);
            contentDic.AddOrUpdate("doctorName", doctorName);
            contentDic.AddOrUpdate("genderName", string.Empty);
            contentDic.AddOrUpdate("scheduleDate", Order.ScheduleDate);
            contentDic.AddOrUpdate("totalFee", Order.TotalFee);
            contentDic.AddOrUpdate("registerTypeId", Order.RegisterTypeId);
            contentDic.AddOrUpdate("registerTypeName", "普通");
            contentDic.AddOrUpdate("doctorTitle", string.Empty);
            contentDic.AddOrUpdate("introduction", "暂无简介");
            contentDic.AddOrUpdate("beginTime", Order.BeginTime);
            contentDic.AddOrUpdate("endTime", Order.EndTime);
            contentDic.AddOrUpdate("emergency", Order.Emergency);
            contentDic.AddOrUpdate("attachInfo", new object());
            contentDic.AddOrUpdate("url", "/patient/register/payment");
            contentDic.AddOrUpdate("preUrl", "/patient/register/preRegistration");
            contentDic.AddOrUpdate("registerCategory", Order.RegisterCategory);
            contentDic.AddOrUpdate("name", Order.UserName);
            contentDic.AddOrUpdate("familyMemberId", Order.FamilyMemberId);
            contentDic.AddOrUpdate("phone", User.Phone);
            contentDic.AddOrUpdate("payMethod", Order.PayMethod);

            var contentJson = contentDic.ToJson();

            return contentJson;
        }
    }
}
