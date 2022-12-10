using System.Collections.Generic;

namespace JkzlSearcher.model
{
    internal class Department
    {

        public string DeptId { get; set; }
        public string DeptName { get; set; }

        public Dictionary<string, Doctor> Doctors { get; set; }

        public Department(string deptId, string deptName)
        {
            DeptId = deptId;
            DeptName = deptName;
        }
    }
}
