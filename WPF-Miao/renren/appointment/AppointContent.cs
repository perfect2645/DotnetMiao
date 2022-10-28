using HttpProcessor.Content;
using renren.common;
using renren.session;

namespace renren.appointment
{
    internal class AppointContent : RenrenBaseContent
    {

        private const string Url = "https://www.medic.ren/PM-server/mobserviceOrder/addServiceOrder";
        public Order Order { get; private set; }

        public AppointContent(Order order) : base(Url)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(MainSession.PlatformSesstion, Constants.UserHospitalId);
            AddContent(MainSession.PlatformSesstion, Constants.HospitalId);
            AddContent(Constants.PatientId, MainSession.UserSession[Constants.PatientId]);
            AddContent(MainSession.PlatformSesstion, Constants.ServiceId);
            AddContent("dateId", Order.DateId);
            AddContent(MainSession.HospitalSession, Constants.TeamId);
            AddContent("bookingDate", Order.BookingDate);
            AddContent(Constants.StartTime, Order.StartTime);
            AddContent(Constants.EndTime, Order.EndTime);
            AddContent("isAgent", false);
            AddContent("fee", 0);
            AddContent(MainSession.PlatformSesstion, Constants.OpenId);
            AddContent(Constants.ServiceStartTime, Order.ServiceStartTime);
            AddContent(Constants.ServiceEndTime, Order.ServiceEndTime);
            AddContent("serviceItem", new string[0]);
            AddContent("bookingType", "0");
            AddContent("bookingToken", MainSession.PlatformSesstion[Constants.PublicKey]);
            AddContent("toFollow", true);
        }
    }
}
