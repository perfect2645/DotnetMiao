using Base.model;
using Base.viewModel;
using Base.viewModel.hospital;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sxjk.viewmodel
{
    internal partial class SxjkViewModel
    {
        private void InitDepartments()
        {
            DateList = new List<DspVal>();

            Departments = new List<HospitalDept>
            {
                new SxjkHospital
                {
                    CityCode = "140900000000",
                    CityName = "忻州市",
                    StationCode = "140902194100",
                    StationName = "忻府区疾控中心接种门诊",
                    DepartmentName = "九价疫苗预约",
                },

                #region 临汾
                new SxjkHospital
                {
                    CityCode = "141000000000",
                    CityName = "临汾市",
                    StationCode = "141002080100",
                    StationName = "尧都区车站街社区服务中心接种门诊",
                    DepartmentName = "九价疫苗预约",
                },
                new SxjkHospital
                {
                    CityCode = "141000000000",
                    CityName = "临汾市",
                    StationCode = "141002030100",
                    StationName = "尧都区水塔街社区服务中心接种门诊",
                    DepartmentName = "九价疫苗预约",
                },
                new SxjkHospital
                {
                    CityCode = "141000000000",
                    CityName = "临汾市",
                    StationCode = "141024010200",
                    StationName = "大槐树镇中心卫生院接种门诊",
                    DepartmentName = "九价疫苗预约",
                },
                #endregion 临汾

                new SxjkHospital
                {
                    CityCode = "140100000000",
                    CityName = "太原市",
                    StationCode = "140105020100",
                    StationName = "平阳社区卫生服务中心",
                    DepartmentName = "九价疫苗预约",
                },
                new SxjkHospital
                {
                    CityCode = "140100000000",
                    CityName = "太原市",
                    StationCode = "140105010100",
                    StationName = "小店区人民医院",
                    DepartmentName = "九价疫苗预约",
                },
                new SxjkHospital
                {
                    CityCode = "140500000000",
                    CityName = "晋城市",
                    StationCode = "140502120100",
                    StationName = "钟家庄社区卫生服务中心",
                    DepartmentName = "九价疫苗预约",
                },
                new SxjkHospital
                {
                    CityCode = "140500000000",
                    CityName = "晋城市",
                    StationCode = "140502020200",
                    StationName = "北街社区卫生服务中心",
                    DepartmentName = "九价疫苗预约",
                },
                new SxjkHospital
                {
                    CityCode = "140200000000",
                    CityName = "大同市",
                    StationCode = "140213140400",
                    StationName = "大同市平城区大同市第一人民医院防保科",
                    DepartmentName = "九价疫苗预约",
                },
                new SxjkHospital
                {
                    CityCode = "140400000000",
                    CityName = "长治市",
                    StationCode = "10001",
                    StationName = "",
                    DepartmentName = "九价疫苗预约",
                    DepartmentId = "101",
                },
                new SxjkHospital
                {
                    CityCode = "140800000000",
                    CityName = "运城市",
                    StationCode = "140802014100",
                    StationName = "高家垣社区卫生服务站成人门诊",
                    DepartmentName = "九价疫苗预约",
                },
            };
        }
    }
}
