using Baohe.constants;
using System.Collections;
using System.Collections.Generic;
using Utils.stringBuilder;

namespace Baohe.appointment
{
    public class Order
    {
        #region properties

        public string OrderKey 
        { 
           get { return $"{MemberSn}_{Index}_{NumberSn}"; }
        }

        public string MemberSn { get; }
        public int Index { get; }
        public string NumberSn { get; private set; }

        public Dictionary<string, object> MemberInfo { get; set; }

        public Dictionary<string, object> MiaoInfo { get; set; }

        public OrderStatus Status { get; set; }

        internal AppointmentContent Content { get; set; }

        #endregion properties

        #region Constructor

        public Order(Dictionary<string, object> memberInfo, int index)
        {
            MemberInfo = memberInfo;
            Index = index;
            MemberSn = memberInfo[Constant.Accountsn].NotNullString();

            OrderStatusEvent.OrderStatusChangedEvent += OrderStatusEvent_OrderStatusChangedEvent;

            InitContent();

        }

        #endregion Constructor

        #region BuildContent

        public void InitContent()
        {
            Content = new AppointmentContent(MemberInfo);

        }

        public void FillContent(session.MiaoSession miaoSession)
        {
            MiaoInfo = (miaoSession["Numbers"] as IList)[Index] as Dictionary<string, object>;
            NumberSn = MiaoInfo["NumberSN"].NotNullString();
            Content.MiaoInfo = MiaoInfo;
            Content.FillContent();

            //Appoint();
        }

        #endregion BuildContent

        #region Appointment

        private void Appoint()
        {
            if (Status != OrderStatus.Pending)
            {
                return;
                // publish to appointmentcontroller
            }
            Status = OrderStatus.Running;

            var orderArgs = new OrderArgs(OrderKey, Content);
            OrderEvent.Publish(OrderKey, orderArgs);
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
