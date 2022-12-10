using JkzlSearcher.model;
using JkzlSearcher.session;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace JkzlSearcher.output
{
    internal class OutputController
    {
        public List<Dictionary<string, object>> DepartmentInput { get; set; }
        public Dictionary<string, object> HospitalInput { get; set; }

        public OutputController(List<Dictionary<string, object>> depts, Dictionary<string, object> hospital)
        {
            DepartmentInput = depts;
            HospitalInput = hospital;
        }

        public string ToHospitalJsoin()
        {
            try
            {
                var hospital = BuildHospital();
                if (hospital == null || !hospital.Departments.HasItem())
                {
                    return string.Empty;
                }
                var json = FileSerializer.Serialize(hospital);

                return json;
            }
            catch(Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, ex.Message);
                return string.Empty;
            }
        }

        private Hospital BuildHospital()
        {
            var hosId = HospitalInput.GetString(Constants.HospitalId);
            var hosName = HospitalInput.GetString(Constants.HosName);
            var hospital = new Hospital(hosId, hosName)
            {
                City = HospitalInput.GetString("cityName"),
                Province = HospitalInput.GetString("provinceName"),
                Address = HospitalInput.GetString("address"),
            };

            var depts = new Dictionary<string, Department>();
            foreach (var departmentIn in DepartmentInput)
            {
                var deptName = departmentIn.GetString(Constants.DeptName);
                if (!ValidDepartment(deptName))
                {
                    continue;
                }
                var deptId = departmentIn.GetString(Constants.HosDeptId);
                var department = new Department(deptId, deptName);
                depts.Add(deptId, department);
            }

            hospital.Departments = depts;

            return hospital;
        }

        private bool ValidDepartment(string deptName)
        {
            return !MainSession.InvalidDepartments.Any(x => deptName.Contains(x));
        }

        public void SaveHospital(string hosJson)
        {
            try
            {
                MainSession.SaveHospital(hosJson);
            }
            catch(Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, ex.Message);
            }
        }
    }
}
