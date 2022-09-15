using System.Collections.Generic;

namespace Jk160.session
{
    public class OrderSession
    {
        public static List<Order> OrderList { get;}

        static OrderSession()
        {
            OrderList = new List<Order>();
        }

        public void AddOrder(Order order)
        {
            OrderList.Add(order);
        }

        public List<Order> GetOrders()
        {
            return OrderList;
        }
    }
}
