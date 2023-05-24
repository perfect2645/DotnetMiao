using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Stringtest
{
    internal class DateTest
    {
        public DateTest()
        {
            DateSubstract();
        }

        private void DateSubstract()
        {
            var currentDate = DateTime.Today;
            var gap = 45070;

            var targetDate = currentDate.AddDays(-gap);
        }
    }
}
