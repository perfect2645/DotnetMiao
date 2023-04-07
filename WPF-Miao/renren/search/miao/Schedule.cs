using System;
using System.Collections.Generic;
using Utils.timerUtil;

namespace renren.search.miao
{
    internal class Schedule
    {
        public bool Id { get; set; }
        public string Date { get; set; }
        public string Half { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ServiceStartTime { get; set; }
        public string ServiceEndTime { get; set; }
        public bool IsOpen { get; set; }
        public bool IsFull { get; set; }
        public IntervalOnTime IntervalOnTime { get; private set; }
        public Action SearchDetailAction { get; set; }

        public Schedule()
        {
            IntervalOnTime = new IntervalOnTime(SearchDetailAction, StartTime, 500);
        }
    }
}
