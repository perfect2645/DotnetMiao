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

        public Hospital Hospital { get; private set; }

        public OutputController(List<Dictionary<string, object>> depts, Dictionary<string, object> hospital)
        {
            DepartmentInput = depts;
            HospitalInput = hospital;
            BuildHospital();
        }

        public void AddDoctors(List<Dictionary<string, object>> doctors)
        {
            if (!HasDepartments())
            {
                return;
            }
            foreach(var doc in doctors)
            {
                var docId = doc.GetString("doctorUid");
                var deptId = doc.GetString("deptId");
                var matchedDept = Hospital.Departments.FirstOrDefault(d => d.Key == deptId).Value;
                if (matchedDept == null)
                {
                    return;
                }
                var doctor = new Doctor
                {
                    DoctorId = docId,
                    DoctorName = doc.GetString("doctorName"),
                };

                matchedDept.Doctors.Add(docId, doctor);
            }

        }

        public string ToHospitalJsoin()
        {
            try
            {
                if (!HasDepartments())
                {
                    return string.Empty;
                }
                var json = FileSerializer.Serialize(Hospital);

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

            Hospital = hospital;
            return hospital;
        }

        private bool ValidDepartment(string deptName)
        {
            var validCheck = MainSession.ValidDepartments.Any(x => deptName.Contains(x));
            if (validCheck)
            {
                return true;
            }
            return !MainSession.InvalidDepartments.Any(x => deptName.Contains(x));
        }

        public void SaveHospital(string hosJson)
        {
            try
            {
                var jsonHospital = ToHospitalJsoin();
                if (string.IsNullOrEmpty(jsonHospital))
                {
                    return;
                }
                MainSession.SaveHospital(hosJson);
            }
            catch(Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, ex.Message);
            }
        }

        internal bool HasDepartments()
        {
            if (Hospital == null || !Hospital.Departments.HasItem())
            {
                return false;
            }

            return true;
        }
    }
}
