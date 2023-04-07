using HttpProcessor.Container;
using Jkchegu.appointment;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jkchegu.session
{
    public class User
    {
        internal AppointController AppointController { get; }
        public string? Session { get; set; }
        public string Etid { get; set; }
        public string Name { get; set; }

        public bool IsSuccess { get; set; }
        public bool IsActive
        {
            get
            {
                return !string.IsNullOrEmpty(Session);
            }
        }

        public string this[string etid]
        {
            get
            {
                if (etid == Etid)
                {
                    return Name;
                }
                return null;
            }
        }

        private readonly object OrderLock = new object();

        public User(string etid, string name)
        {
            Etid = etid;
            Name = name;
            AppointController = HttpServiceController.GetService<AppointController>();
        }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 用户信息 *********");

            sb.AppendLine($"Etid - {Etid}");
            sb.AppendLine($"Name - {Name}");
            sb.AppendLine($"Session - {Session}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }

        public void Yuyue(List<Order> orderList)
        {
            lock(OrderLock)
            {
                AppointController.Yuyue(this, orderList);
            }
        }
    }
}
