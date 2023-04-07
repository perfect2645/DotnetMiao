using JkzlSearcher.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JkzlSearcher.search
{
    internal class HospitalContent : JkzlContent
    {
        private const string Url = "https://appoint.yihu.com/appoint/do/hospital/queryHospitalById";
        public string HospitalId { get; private set; }
        public HospitalContent(string hosId) : base(Url)
        {
            HospitalId = hosId;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("hosId", HospitalId);
        }
    }
}
