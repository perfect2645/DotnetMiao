using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baohe.viewModel.platform
{
    public class ExchangeInfo
    {
        public string ArrangeId { get; set; }
        public string NumberId { get; set; }
        public string SerialNo { get; set; }
        public string CommendScope { get; set; }
        public ExchangeInfo(string arrangeId, string numberId, string serialNo, string commendScope)
        {
            ArrangeId = arrangeId;
            NumberId = numberId;
            SerialNo = serialNo;
            CommendScope = commendScope;
        }
    }
}
