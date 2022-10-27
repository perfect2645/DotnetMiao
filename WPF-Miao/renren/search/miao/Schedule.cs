using System.Collections.Generic;

namespace renren.search.miao
{
    internal class Schedule
    {
        public bool Id { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsOpen { get; set; }
        public bool IsFull { get; set; }
    }
}
