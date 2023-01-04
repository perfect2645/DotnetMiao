using Tianhe.common;
using Tianhe.session;
using System.Text;
using Utils;

namespace Tianhe.appointment
{
    internal class PreOrderContent : TianheContent
    {
        public Order Order { get; }

        private static readonly string url = "https://ctmingyi.com:18082/api/order/preOrder";

        public PreOrderContent() : base(url)
        {
            BuildHeader();
        }
        public PreOrderContent(Order schedule) : base(url)
        {
            Order = schedule;
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            var token = MainSession.PlatformSession.GetString(Constants.Token);
            AddHeader("Authorization", $"Bearer {token}");
        }

        private void BuildContent()
        {
            AddContent("userId", Order.UserId);
            AddContent("doctorId", Order.DoctorId);
            AddContent("familyId", Order.FamilyId);
            AddContent("scheduleId", Order.ScheduleId);
            AddContent("parttimeId", Order.ParttimeId);
            AddContent("hospitalid", Order.Hospitalid);
        }
    }
}
