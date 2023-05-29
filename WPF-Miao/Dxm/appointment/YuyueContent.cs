using Dxm.common;
using Dxm.login;
using System;
using System.Collections.Generic;

namespace Dxm.appointment
{
    internal class YuyueContent : DxmContent
    {
        private static string baseUrl = "https://mix.med.gzhc365.com/api/register/generatororder";
        public Order Order { get; private set; }
        public YuyueContent(Order order, DxmLogin user) : base(baseUrl, user)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("hisId", Order.HisId);
            AddContent("platformId", Order.PlatformId);
            AddContent("platformSource", Order.PlatformSource);
            AddContent("subSource", Order.SubSource);
            AddContent("subHisId", Order.SubHisId);
            AddContent("deptId", Order.DeptId);
            AddContent("doctorId", Order.DoctorId);
            AddContent("outExtFieldsFlag", 1);
            AddEncodeContent("extFields", BuildExtFields());
            AddContent("patientId", Order.PatientId);
            AddContent("payFlag", 1);
            AddEncodeContent("transParam", BuildTransParam());
            AddContent("scheduleDate", Order.ScheduleDate);
            AddContent("scheduleId", Order.ScheduleId);
            AddContent("visitPeriod", Order.VisitPeriod);
            AddEncodeContent("visitBeginTime", Order.VisitBeginTime);
            AddEncodeContent("visitEndTime", Order.VisitEndTime);
            AddContent("_hcSource", string.Empty);
            AddContent("login_access_token", Order.Token);
        }

        private Dictionary<string, object> BuildTransParam()
        {
            var transParam = new Dictionary<string, object>();
            transParam.Add("type", "hcTransParam");
            transParam.Add("plat", "gzhc365zhyy");
            transParam.Add("birthday", User.Birthday);
            transParam.Add("Address", User.Address);

            return transParam;
        }

        private Dictionary<string, object> BuildExtFields()
        {
            var extFields = new Dictionary<string, object>();
            extFields.Add("_bdaiGuide", string.Empty);
            extFields.Add("_doctorQrGuide", string.Empty);
            extFields.Add("_deptQrGuide", string.Empty);
            extFields.Add("_hcSource", string.Empty);

            return extFields;
        }
    }
}
