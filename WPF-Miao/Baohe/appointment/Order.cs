namespace Baohe.appointment
{
    internal class Order
    {
        #region properties

        public string OrderKey 
        { 
           get { return $"{MemberSn}_{NumberSn}"; }
        }

        public string MemberSn { get; }
        public string NumberSn { get; }

        public OrderStatus Status { get; set; }

        public AppointmentContent Content { get; set; }

        #endregion properties

        #region Constructor

        public Order(string memberSn)
        {
            MemberSn = memberSn;

            OrderStatusEvent.OrderStatusChangedEvent += OrderStatusEvent_OrderStatusChangedEvent;
        }

        public Order(string memberSn, string numberSn)
        {
            MemberSn = memberSn;
            NumberSn = numberSn;
            OrderKey = $"{memberSn}_{numberSn}";

            OrderStatusEvent.OrderStatusChangedEvent += OrderStatusEvent_OrderStatusChangedEvent;
        }

        #endregion Constructor

        #region BuildContent

        public void InitContent()
        {

        }

        public void FillContent()
        {
            Appoint();
        }

        #endregion BuildContent

        #region Appointment

        private void Appoint()
        {
            if (Status == OrderStatus.Pending)
            {
                Status = OrderStatus.Running;
                // publish to appointmentcontroller
            }

        }

        #endregion Appointment

        #region Status

        private void OrderStatusEvent_OrderStatusChangedEvent(object? sender, OrderStatusArgs e)
        {
            if (e.OrderKey != OrderKey)
            {
                return;
            }

            Status = e.Status;
        }

        #endregion Status
    }
}
