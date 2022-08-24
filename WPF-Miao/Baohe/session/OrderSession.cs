using Baohe.appointment;
using System.Collections.Generic;

namespace Baohe.session
{
    public class OrderSession
    {
        public static HashSet<Order> OrderList { get;}

        static OrderSession()
        {
            OrderList = new HashSet<Order>();
        }

        public void AddOrder(Order order)
        {
            OrderList.Add(order);
        }
    }
}
