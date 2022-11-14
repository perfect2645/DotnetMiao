using chutian.common;
using chutian.session;
using System.Text;
using Utils;

namespace chutian.appointment
{
    internal class PreOrderContent : ChutianBaseContent
    {
        private Order _schedule;

        private static readonly string url = "https://ctmingyi.com:18082/api/order/preOrder";

        public PreOrderContent() : base(url)
        {
            BuildHeader();
        }
        public PreOrderContent(Order schedule) : base(url)
        {
            _schedule = schedule;
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
            AddContent("userId", _schedule.UserId);
            AddContent("doctorId", _schedule.DoctorId);
            AddContent("familyId", _schedule.FamilyId);
            AddContent("scheduleId", _schedule.ScheduleId);
            AddContent("parttimeId", _schedule.ParttimeId);
            AddContent("hospitalid", _schedule.Hospitalid);
        }
    }
}
