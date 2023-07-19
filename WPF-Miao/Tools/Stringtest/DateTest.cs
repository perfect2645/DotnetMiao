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
            //var offset = 45070;

            var offset = 45125;
            var initDate = new DateTime(1900, 1, 1);
            var targetDate = initDate.AddDays(offset);
        }
    }
}
