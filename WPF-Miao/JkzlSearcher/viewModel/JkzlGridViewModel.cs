using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JkzlSearcher.viewModel
{
    internal class JkzlGridViewModel
    {
        public ObservableCollection<JkzlGridRow> JkzlGridData { get; set; }

        public JkzlGridViewModel()
        {
            JkzlGridData = new ObservableCollection<JkzlGridRow>();

            var row = new JkzlGridRow
            {
                DeptId = "DeptId",
                DeptName = "DeptName",
                HospitalId = "HospitalId",
                HospitalName = "HospitalName",
            };

            JkzlGridData.Add(row);
        }
    }
}
