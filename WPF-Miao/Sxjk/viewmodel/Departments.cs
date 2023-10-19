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
                #region 晋中
                new SxjkHospital
                {
                    CityCode = "140700000000",
                    CityName = "晋中市",
                    StationCode = "140726010500",
                    StationName = "晋中市太谷区明星卫生院古城普通成人接种门诊",
                    DepartmentName = "九价疫苗预约",
                },
                new SxjkHospital
                {
                    CityCode = "140700000000",
                    CityName = "晋中市",
                    StationCode = "140722010100",
                    StationName = "山西省晋中市左权县辽阳社区",
                    DepartmentName = "九价疫苗预约",
                },
                #endregion 晋中
                #region 长治
                new SxjkHospital
                {
                    CityCode = "140400000000",
                    CityName = "长治市",
                    StationCode = "140406080100",
                    StationName = "潞城区潞华街道办事处社区卫生服务中心",
                    DepartmentName = "九价疫苗预约",
                },
                new SxjkHospital
                {
                    CityCode = "140400000000",
                    CityName = "长治市",
                    StationCode = "140427010300",
                    StationName = "壶关县妇幼保健计划生育服务中心",
                    DepartmentName = "九价疫苗预约",
                },
                #endregion 长治
                #region 阳泉
                new SxjkHospital
                {
                    CityCode = "140300000000",
                    CityName = "阳泉市",
                    StationCode = "140321010100",
                    StationName = "阳泉市平定县 冠山镇卫生院",
                    DepartmentName = "九价疫苗预约",
                },
                new SxjkHospital
                {
                    CityCode = "140300000000",
                    CityName = "阳泉市",
                    StationCode = "140322010600",
                    StationName = "阳泉市盂县人民医院预防接种门诊",
                    DepartmentName = "九价疫苗预约",
                }, 
                #endregion 阳泉
                #region 朔州
                new SxjkHospital
                {
                    CityCode = "140600000000",
                    CityName = "朔州市",
                    StationCode = "140602321500",
                    StationName = "朔城区文远路门诊",
                    DepartmentName = "九价疫苗预约",
                },
                #endregion 朔州
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
                #region 忻州
                new SxjkHospital
                {
                    CityCode = "140900000000",
                    CityName = "忻州市",
                    StationCode = "140902194100",
                    StationName = "忻府区疾控中心接种门诊",
                    DepartmentName = "九价疫苗预约",
                },
                #endregion 忻州
                #region 太原
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

                #endregion 太原
                #region 晋城
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
                #endregion 晋城
                #region 大同
                new SxjkHospital
                {
                    CityCode = "140200000000",
                    CityName = "大同市",
                    StationCode = "140213150100",
                    StationName = "大同市平城区马军营卫生院",
                    DepartmentName = "九价疫苗预约",
                }, 
                new SxjkHospital
                {
                    CityCode = "140200000000",
                    CityName = "大同市",
                    StationCode = "140213140400",
                    StationName = "平城区大同市第一人民医院防保科",
                    DepartmentName = "九价疫苗预约",
                },
                #endregion 大同
                #region 运城
                new SxjkHospital
                {
                    CityCode = "140800000000",
                    CityName = "运城市",
                    StationCode = "140802014100",
                    StationName = "高家垣社区卫生服务站成人门诊",
                    DepartmentName = "九价疫苗预约",
                },
                #endregion 运城
                #region 吕梁
                new SxjkHospital
                {
                    CityCode = "141100000000",
                    CityName = "吕梁市",
                    StationCode = "140406080100",
                    StationName = "柳林县中医院预防接种门诊",
                    DepartmentName = "九价疫苗预约",
                },
                new SxjkHospital
                {
                    CityCode = "141100000000",
                    CityName = "吕梁市",
                    StationCode = "141182030200",
                    StationName = "太和桥社区卫生服务中心",
                    DepartmentName = "九价疫苗预约",
                },
                #endregion 吕梁
            };
        }
    }
}
