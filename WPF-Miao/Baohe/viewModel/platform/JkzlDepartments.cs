using Baohe.viewModel.platform;
using Base.viewModel.hospital;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baohe.viewModel
{
    internal partial class BaoheViewModel
    {
        internal void BuildDepartments()
        {
            Departments = new List<HospitalDept>();

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1099108", "雨花经济开发区社区卫生服务中心",
                "7235364", "九价Hpv")
            {
                DoctorSn = "711230166"
            });

            Departments.Add(new Jiankangzhilu("9001471", "包河区同安街道社区卫生服务中心",
                "1038404", "包河区同安街道社区卫生服务中心",
                "7246744", "九价HPV")
            {
                HasYzm = true,
                DoctorSn = "711282925"
            });

            Departments.Add(new Jiankangzhilu("9001471", "包河区同安街道社区卫生服务中心",
                "1038404", "包河区同安街道社区卫生服务中心",
                "7245673", "四价HPV")
            {
                HasYzm = true,
                DoctorSn = "711279474"
            });

            Departments.Add(new Jiankangzhilu("9000981", "包河区万年埠街道",
                "1094367", "包河区万年埠街道社区卫生服务中心",
                "7231670", "九价Hpv")
            {
                DoctorSn = "711212600"
            });

            #region 漳州

            Departments.Add(new Jiankangzhilu("1000031", "漳州市龙文区步",
                "1101211", "漳州市龙文区步文街道社区卫生服务中心",
                "7242593", "九价Hpv")
            {
                HasYzm = false,
                DoctorSn = "711267962"
            });

            Departments.Add(new Jiankangzhilu("1000031", "漳州市龙文区步",
                "1101211", "漳州市龙文区步文街道社区卫生服务中心",
                "7242593", "四价Hpv")
            {
                HasYzm = false,
                DoctorSn = "711267961"
            });

            #endregion 漳州

            #region 南京

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1099108", "雨花经济开发区社区卫生服务中心",
                "7235364", "九价Hpv")
            {
                DoctorSn = "711230166"
            });

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1094218", "西善桥",
                "7243224", "九价Hpv")
            {
                DoctorSn = "711269136"
            });

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1094417", "雨花社区卫生服务中心",
                "7225728", "九价Hpv")
            {
                HasYzm = false,
                DoctorSn = "711170881"
            });

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1094417", "雨花社区卫生服务中心",
                "7208870", "预防接种预约")
            {
                HasYzm = false,
                DoctorSn = "711102799" //儿童9价宫颈癌疫苗
            });

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1094417", "雨花社区卫生服务中心",
                "7225728", "九价Hpv")
            {
                HasYzm = false,
                DoctorSn = "711170881"
            });

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1094417", "雨花社区卫生服务中心",
                "7225728", "四价Hpv")
            {
                HasYzm = false,
                DoctorSn = "711170863"
            });

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1092338", "板桥社区卫生服务中心",
                "7234217", "九价Hpv")
            {
                HasYzm = false,
                DoctorSn = "710795068"
            });

            #endregion 南京

            #region 合肥

            Departments.Add(new Jiankangzhilu("9000981", "包河区万年埠街道",
                "1094367", "包河区万年埠街道社区卫生服务中心",
                "7231670", "九价Hpv")
            {
                DoctorSn = "711212600"
            });

            Departments.Add(new Jiankangzhilu("9000981", "包河区万年埠街道",
                "1094367", "包河区万年埠街道社区卫生服务中心",
                "7231733", "四价Hpv")
            {
                DoctorSn = "711212504"
            });

            Departments.Add(new Jiankangzhilu("9001150", "蜀山稻香村街道",
                "1031995", "稻香村街道社区卫生服务中心",
                "7231814", "九价Hpv")
            {
                DoctorSn = "711213207"
            });

            Departments.Add(new Jiankangzhilu("9001150", "蜀山稻香村街道",
                "1031995", "稻香村街道社区卫生服务中心",
                "7231800", "四价Hpv")
            {
                DoctorSn = "711213217"
            });

            Departments.Add(new Jiankangzhilu("9000370", "蜀山区南岗镇卫生院",
                "1040231", "蜀山区南岗镇卫生院",
                "7211903", "九价Hpv")
            {
                DoctorSn = "711091344"
            });

            Departments.Add(new Jiankangzhilu("9000370", "蜀山区南岗镇卫生院",
                "1040231", "蜀山区南岗镇卫生院",
                "7211892", "四价Hpv")
            {
                DoctorSn = "711091352"
            });

            Departments.Add(new Jiankangzhilu("9000393", "包河区包公街道",
                "1039346", "包河区包公街道社区服务中心",
                "7229969", "九价Hpv")
            {
                DoctorSn = "711199332",
            });
            Departments.Add(new Jiankangzhilu("9000393", "包河区包公街道",
                "1039346", "包河区包公街道社区服务中心",
                "7230002", "四价Hpv")
            {
                DoctorSn = "711199340",
            });

            Departments.Add(new Jiankangzhilu("9001026", "蜀山区井岗中心服务号",
                "1047063", "蜀山井岗镇社区卫生服务中心",
                "7229195", "九价Hpv")
            {
                DoctorSn = "711188793"
            });

            Departments.Add(new Jiankangzhilu("9001026", "蜀山区井岗中心服务号",
                "1047063", "蜀山井岗镇社区卫生服务中心",
                "7229244", "四价Hpv")
            {
                DoctorSn = "711188785"
            });

            Departments.Add(new Jiankangzhilu("9000415", "包河区常青街道社区卫生服务中心",
                "1047032", "常青街道薛河湾社区卫生服务站",
                "7237655", "九价HPV疫苗")
            {
                HasYzm = true,
                DoctorSn = "711238182"
            });
            Departments.Add(new Jiankangzhilu("9000415", "包河区常青街道社区卫生服务中心",
                "1047032", "常青街道薛河湾社区卫生服务站",
                "7237655", "四价HPV疫苗")
            {
                HasYzm = false,
                DoctorSn = "711247350"
            });

            Departments.Add(new Jiankangzhilu("9000371", "合肥蜀山五里墩",
                "1033881", "蜀山区五里墩街道社区卫生服务中心",
                "7229497", "九价HPV疫苗")
            {
                HasYzm = true,
                DoctorSn = "711190104"
            });

            Departments.Add(new Jiankangzhilu("9000371", "合肥蜀山五里墩",
                "1033881", "蜀山区五里墩街道社区卫生服务中心",
                "7229497", "四价HPV疫苗")
            {
                HasYzm = true,
                DoctorSn = "711190063"
            });

            Departments.Add(new Jiankangzhilu("9001016", "蜀山区西园街道",
                "1031364", "蜀山区西园街道社区卫生服务中心",
                "7210427", "九价HPV疫苗")
            {
                HasYzm = true,
                DoctorSn = "711188596"
            });

            Departments.Add(new Jiankangzhilu("9001016", "蜀山区西园街道",
                "1031364", "蜀山区西园街道社区卫生服务中心",
                "7210427", "四价HPV疫苗")
            {
                HasYzm = true,
                DoctorSn = "711082296"
            });

            #endregion 合肥

            Departments.Add(new Jiankangzhilu("9000553", "武汉硚口区",
                "1023044", "硚口区古田街社区卫生服务中心",
                "7152219", "九价HPV疫苗")
            {
                HasYzm = false,
                DoctorSn = "711250004"
            });

            Departments.Add(new Jiankangzhilu("9000553", "武汉硚口区",
                "1023044", "硚口区古田街社区卫生服务中心",
                "7152219", "四价HPV疫苗")
            {
                HasYzm = false,
                DoctorSn = "711266375"
            });

            Departments.Add(new Jiankangzhilu("9000553", "武汉硚口区",
                "1023020", "硚口区汉正街社区卫生服务中心",
                "7234379", "九价HPV疫苗")
            {
                HasYzm = false,
                DoctorSn = "711226997"
            });

            //        Departments.Add(new Jiankangzhilu("9001026", "蜀山区井岗中心服务号",
            //"1047063", "蜀山区经开区井岗镇社区卫生服务中心",
            //"7229244", "四价Hpv"));

            SelectedDepartment = Departments.FirstOrDefault();
        }
    }
}
