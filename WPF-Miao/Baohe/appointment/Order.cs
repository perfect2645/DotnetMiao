using Baohe.appointment.content;
using Baohe.constants;
using Baohe.session;
using System.Collections;
using System.Collections.Generic;
using Utils;
using Utils.datetime;
using Utils.number;
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
            Content = ContentMapper.GetContent(BaoheSession.PlatformSesstion.GetString(Constant.HospitalId));
            Content.InitContent(MemberInfo);
        }

        public void FillContent(session.MiaoSession miaoSession)
        {
            var numbers = miaoSession["Numbers"] as IList;
            //var seed = BaoheSession.GetRandomSeed();
            var index = NumberUtil.IntRandom(0, numbers.Count - 1);

            MiaoInfo = numbers[index] as Dictionary<string, object>;
            NumberSn = MiaoInfo["NumberSN"].NotNullString();
            Content.MiaoInfo = MiaoInfo;
            Content.FillContent();

            Appoint();
        }

        #endregion BuildContent

        #region Appointment

        private void Appoint()
        {
            if (Status != OrderStatus.Pending)
            {
                return;
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
