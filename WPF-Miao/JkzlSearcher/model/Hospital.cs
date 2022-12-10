using System.Collections.Generic;

namespace JkzlSearcher.model
{
    internal class Hospital
    {

        public string HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public Dictionary<string, Department> Departments { get; set; }

        public Hospital(string hospitalId, string hospitalName)
        {
            HospitalId = hospitalId;
            HospitalName = hospitalName;
            Departments = new Dictionary<string, Department>();
        }
    }
}
