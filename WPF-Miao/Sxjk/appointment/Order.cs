using Sxjk.login;
using System.Text;

namespace Sxjk.appointment
{
    public class Order
    {
        public string UserName { get; set; }

        public string Child_code { get; set; }
        public string Station_code { get; set; }
        public string Vaccine_code { get; set; }
        public string Reservation_date { get; set; }
        public string Reservation_begin_time { get; set; }
        public string Reservation_end_time { get; set; }
        public string Price_id { get; set; }
        public string Address_info { get; set; } = "{\"address\":\"\",\"city\":\"测试市\",\"district\":\"\",\"latitude\":\"\",\"longitude\":\"\",\"province\":\"\",\"streetName\":\"\",\"streetNumber\":\"\"}";
        public string LoginUser_name { get; set; }
        public string City_code { get; set; }
        public string ResultMsg { get; set; }

        internal SxjkLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"LoginUser_name - {LoginUser_name}");
            sb.AppendLine($"Child_code - {Child_code}");
            sb.AppendLine($"City_code - {City_code}");
            sb.AppendLine($"Station_code - {Station_code}");
            sb.AppendLine($"Vaccine_code - {Vaccine_code}");
            sb.AppendLine($"Reservation_date - {Reservation_date}");
            sb.AppendLine($"Time - {Reservation_begin_time} - {Reservation_end_time}");
            sb.AppendLine($"Price_id - {Price_id}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
