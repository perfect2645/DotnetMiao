using jinyinhu.common;
using Utils;

namespace jinyinhu.appointment
{
    internal class YuyueContent : JinyinhuContent
    {
        private static string url = "http://wx1936.cnhis.cc/wx/user/appointment/reg.htm";
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
            AddContent("timeId", Order.TimeId);
            AddContent("reservationDate", Order.ReservationDate);
            AddContent("userId", Order.UserId);
            AddContent("yeWuId", Order.YeWuId);
            AddContent("reservationTime", Order.ReservationTime);
            AddContent("appointmentType", Order.AppointmentType);
        }
    }
}
