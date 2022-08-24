using Baohe.constants;
using System.Collections.Generic;
using Utils.stringBuilder;

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
        public string NumberSn { get; private set; }

        public Dictionary<string, object> MemberInfo { get; set; }

        public Dictionary<string, object> MiaoInfo { get; set; }

        public OrderStatus Status { get; set; }

        public AppointmentContent Content { get; set; }

        #endregion properties

        #region Constructor

        public Order(Dictionary<string, object> memberInfo)
        {
            MemberInfo = memberInfo;
            MemberSn = memberInfo[Constant.AccountSn].NotNullString();

            OrderStatusEvent.OrderStatusChangedEvent += OrderStatusEvent_OrderStatusChangedEvent;

            InitContent();

        }

        #endregion Constructor

        #region BuildContent

        public void InitContent()
        {
            Content = new AppointmentContent();

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
