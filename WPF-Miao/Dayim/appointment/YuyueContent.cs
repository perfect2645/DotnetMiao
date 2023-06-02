using Dayim.common;
using Dayim.login;
using System.Collections.Generic;

namespace Dayim.appointment
{
    internal class YuyueContent : DayimContent
    {
        private static string baseUrl = "https://mp.dywrbt.com/vac/wechat/smallRoutine/pay";
        public Order Order { get; private set; }
        public YuyueContent(Order order, DayimLogin user) : base(baseUrl, user)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("vaccinPeopleId", Order.UserId);
            AddContent("stockId", Order.StockId);
            AddContent("vaccinateTimeDetailId", Order.VaccinateTimeDetailId);
            AddContent("entCompanyId", Order.EntCompanyId);
            AddContent("vacId", Order.VacId);
            AddContent("reserveDate", Order.ReserveDate);
            AddContent("reserveDateList", BuildDateList());
            AddContent("mode", "1");
            AddContent("vacTypeId", Order.VacTypeId);
            AddContent("queueId", Order.QueueId);
        }

        private string[] BuildDateList()
        {
            var dateList = new List<string>();
            dateList.Add(Order.ReserveDate);

            return dateList.ToArray();
        }

    }
}
