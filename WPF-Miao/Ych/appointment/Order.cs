using System.Text;
using Utils.timerUtil;

namespace Ych.appointment
{
    public class Order
    {
        public string DepartmentCode { get; set; }
        public string DoctorCode { get; set; }
        public string AppointmentType { get; set; }
        public string scheduleDate { get; set; }
        public string TimeFlag { get; set; }
        public string BeginTimeEncode { get; set; }
        public string EndTimeEncode { get; set; }
        public string ScheduleDetailId { get; set; }
        public string RegFee { get; set; }
        public string PatientCode { get; set; }
        public string PatientType { get; set; } = "1";
        public string PatientMedicalCardType { get; set; } = "1";
        public string PatientNameEncode { get; set; }
        public string PatientMobile { get; set; }
        public string PatientIdentityCardNumber { get; set; }


        public Order()
        {
        }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 开始预约 *********");

            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine($"YeWuId - {YeWuId}");
            sb.AppendLine($"AppointmentType - {AppointmentType}");
            sb.AppendLine($"ReservationDate - {ReservationDate}");
            sb.AppendLine($"ReservationTime - {ReservationTime}");
            sb.AppendLine($"TimeId - {TimeId}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
