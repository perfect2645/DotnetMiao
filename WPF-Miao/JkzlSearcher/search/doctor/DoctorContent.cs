using JkzlSearcher.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utils.datetime;

namespace JkzlSearcher.search
{
    internal class DoctorContent : JkzlContent
    {
        private const string Url = "https://appoint.yihu.com/appoint/do/doctor/getDocListByDeptId";

        public string DeptId { get; set; }
        public string HospitalId { get; set; }

        public DoctorContent(string deptId, string hosId) : base(Url)
        {
            DeptId = deptId;
            HospitalId = hosId;
            BuildContent();
        }

        public override void BuildDefaultHeaders(HttpClient httpClient)
        {
            base.BuildDefaultHeaders(httpClient);
        }

        private void BuildContent()
        {
            AddContent("t", DateTimeUtil.GetTimeStamp());
            AddContent("hosId", HospitalId);
            AddContent("deptId", DeptId);
            AddContent("pageIndex", "1");
            AddContent("pageSize", "15");
            AddContent("multiHosDeptId", "");
        }
    }
}
