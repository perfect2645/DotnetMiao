using jinyinhu.common;
using Utils;

namespace jinyinhu.appointment
{
    internal class YuyueContent : JinyinhuContent
    {
        private static string url = "http://101.34.141.250:9653/api/front/appointment/save";
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
