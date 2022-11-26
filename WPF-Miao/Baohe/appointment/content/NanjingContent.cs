using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baohe.appointment
{
    internal class NanjingContent : AppointmentContent
    {
        public NanjingContent(Dictionary<string, object> memberInfo) : base(memberInfo)
        {
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            throw new NotImplementedException();
        }

        private void BuildContent()
        {
            throw new NotImplementedException();
        }
    }
}
