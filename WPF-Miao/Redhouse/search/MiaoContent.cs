using Redhouse.common;
using System;
using Utils.datetime;

namespace Redhouse.search
{
    internal class MiaoContent : RedhouseContent
    {
        public MiaoContent(string url) : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("endAttDate", BuildEndAttDate());
            //AddContent("endAttDate", GetWednesday());
            AddContent("last", 8);
            AddContent("amount", 8);
        }

        private string BuildEndAttDate()
        {
            //var nextWeekDay = DateTimeUtil.GetDayOfNextWeek(DateTime.Today.DayOfWeek);
            //return nextWeekDay;

            var dayToday = DateTimeUtil.GetDayOfWeek(DateTime.Today.DayOfWeek);
            return dayToday;
        }

        private string GetWednesday()
        {
            var dayToday = DateTimeUtil.GetDayOfWeek(DayOfWeek.Wednesday);
            return dayToday;
        }
    }
}
