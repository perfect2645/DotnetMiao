using Zhi.common;
using Utils;
using Utils.datetime;
using System.Linq;
using Utils.stringBuilder;

namespace Zhi.appointment
{
    internal class YuyueContent : ZhiContent
    {
        private static string url = "http://www.szZhirmyy.com/wechatclient/api/auth/appointment/confirmAppointment";
        public Order Order { get; }
        public YuyueContent(Order order) : base(url)
        {
            Order = order;
            BuildContent();
        }

        protected override void BuildHeader()
        {
            base.BuildHeader();
        }

        private void BuildContent()
        {
            AddContent("departmentCode", Order.DepartmentCode);
            AddContent("doctorCode", Order.DoctorCode);
            AddContent("appointmentType", Order.AppointmentType);
            AddContent("scheduleDate", Order.ScheduleDate);
            AddContent("timeFlag", Order.TimeFlag);
            AddContent("beginTime", Order.BeginTimeEncode);
            AddContent("endTime", Order.EndTimeEncode);
            AddContent("scheduleDetailId", Order.ScheduleDetailId);
            AddContent("regFee", Order.RegFee);
            AddContent("patientCode", Order.PatientCode);
            AddContent("patientType", Order.PatientType);
            AddContent("patientMedicalCardType", Order.PatientMedicalCardType);
            AddContent("patientMedicalCardNumber", Order.PatientIdentityCardNumber);
            AddContent("patientName", Order.PatientNameEncode);
            AddContent("patientMobile", Order.PatientMobile);
            AddContent("outOrderNumber", string.Empty);
            AddContent("patientIdentityCardType", 1);
            AddContent("patientIdentityCardNumber", Order.PatientIdentityCardNumber);
            AddContent("outpatientTpye", 1);

            var timestamp = DateTimeUtil.GetTimeStamp();
            AddContent("timestamp", timestamp);

            AddContent("sign", GetContentsMD5());
        }
    }
}
