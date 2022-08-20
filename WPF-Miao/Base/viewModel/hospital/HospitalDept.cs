using Utils;

namespace Base.viewModel.hospital
{
    public abstract class HospitalDept : NotifyChanged, IHospital, IDepartment, IPlatform
    {
        private string _hospitalId;
        public string HospitalId 
        {
            get { return _hospitalId; }
            set
            {
                _hospitalId = value;
                NotifyUI(() => HospitalId);
            }
        }

        private string _hospitalName;
        public string HospitalName
        {
            get { return _hospitalName; }
            set
            {
                _hospitalName = value;
                NotifyUI(() => HospitalName);
            }
        }

        private string _departmentId;
        public string DepartmentId
        {
            get { return _departmentId; }
            set
            {
                _departmentId = value;
                NotifyUI(() => DepartmentId);
            }
        }

        private string _departmentName;
        public string DepartmentName
        {
            get { return _departmentName; }
            set
            {
                _departmentName = value;
                NotifyUI(() => DepartmentName);
            }
        }

        private string _platformId;
        public string PlatformId
        {
            get { return _platformId; }
            set
            {
                _platformId = value;
                NotifyUI(() => PlatformId);
            }
        }

        private string _platformName;
        public string PlatformName
        {
            get { return _platformName; }
            set
            {
                _platformName = value;
                NotifyUI(() => PlatformName);
            }
        }

        public string Display
        {
            get { return $"{HospitalName}-{DepartmentName}"; }
        }

        public abstract string ToLogString();
    }
}
