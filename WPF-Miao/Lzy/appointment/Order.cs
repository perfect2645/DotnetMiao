using Lzy.login;
using System;
using System.Text;
using Utils;
using Utils.datetime;
using Utils.stringBuilder;

namespace Lzy.appointment
{
    public class Order
    {
        public string UserName { get; set; }

        public string Op { get; set; }
        public string TimeId { get; set; }
        public string Mobile { get; set; }
        public string DeptId { get; set; }

        private string _date;
        public string Date 
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                SetWeek();
            }
        }

        public string Week { get; private set; }

        public string ResultMsg { get; set; }

        internal LzyLogin User { get; set; }

        private void SetWeek()
        {
            var dayOfWeek = DateTimeUtil.GetWeek(Date);
            Week = dayOfWeek.NotNullString();
        }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"Date - {Date}");
            sb.AppendLine($"TimeId - {TimeId}");
            sb.AppendLine($"Week - {Week}");
            sb.AppendLine($"Monile - {Mobile}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
