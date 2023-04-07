using Utils.timerUtil;

namespace Jkchegu.search
{
    internal class SearchProcessor
    {
        public IntervalOnTime SearchInterval { get; set; }
        public string Date { get; set; }
        public bool IsGet { get; set; }

        public IntervalOnTime this[string date]
        {
            get
            {
                if (Date.Equals(date))
                {
                    return SearchInterval;
                }

                return null;
            }
        }
    }
}
