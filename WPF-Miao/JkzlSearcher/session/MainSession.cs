using Base.session;
using Base.viewmodel.status;
using Logging.logModel;
using NLog;
using System.Collections.Generic;

namespace JkzlSearcher.session
{
    internal class MainSession : MainSessionBase
    {
        public static string Cookie { get; set; }
        public static Logger OutputLogger { get; private set; }
        public static int CurrentHospitalId { get; set; } = 1015685;
        private static readonly object _hospitalIdLock = new object();
        public static Dictionary<string, object> PlatformSession { get; private set; }
        static MainSession()
        {
            OutputLogger = new NLogTemplate("../../../output/JkzlHospital.json").Logger;
            PlatformSession = new Dictionary<string, object>();
        }

        public static string GetNextHospitalId()
        {
            lock(_hospitalIdLock)
            {
                CurrentHospitalId++;
                return CurrentHospitalId.ToString();
            }
        }

        public static void SaveHospital(string hosJson)
        {
            OutputLogger.Info($"{hosJson},");
        }


        #region MiaoStatus

        public static void SetStatus(MiaoProgress miaoProgress)
        {
            MiaoStatus.MiaoProgress = miaoProgress;
        }

        public static MiaoProgress GetStatus()
        {
            return MiaoStatus.MiaoProgress;
        }

        public static void SetStatus(MiaoProgress miaoProgress, object data)
        {
            MiaoStatus.OnStatusUpdate(miaoProgress, data);
        }

        #endregion MiaoStatus

        #region Valid Department

        internal static HashSet<string> ValidDepartments = new HashSet<string>
        {
            "社区", "站", "疫苗", "宫颈", "HP", "Hp", "hp", "首针", "预防",
            "妇幼", "预约", "挂号"
        };

        internal static HashSet<string> InvalidDepartments = new HashSet<string>
        {
            "外", "内", "儿", "妇科", "口腔", "皮肤", "眼", "血", "病", "症","神经",
            "耳喉","鼻","喉", "中医", "心", "针灸", "影像", "麻醉", "放射", "骨",
            "急诊", "诊断", "康复", "肾", "瘤", "痛", "氧", "胃", "门诊", "视",
            "咨询", "精神", "理疗", "泌尿", "美容", "感染", "脑","乳房", "拿", "腹",
            "老年", "病理", "乳腺", "脊柱", "重症", "产", "药", "检验", "消化",
            "医学", "白", "五官", "男", "痘", "银屑", "牛皮", "孕", "育", "牙",
            "介", "肝", "呼吸", "体检", "急救", "肛", "肠", "微创", "中西", "湿",
            "性病", "激光", "哮喘", "失眠", "传染", "肌", "肺", "生殖", "热", "未",
            "方便", "伤", "摩", "镜", "功能", "手", "足", "磁", "包", "超", "酸",
            "胆", "石", "护理", "椎","干诊", "全科", "风", "循环", "镶", "营养",
            "镇", "CT", "放疗", "ICU", "脉", "疤", "水疗", "特检", "整", "技", "移植",
            "综合", "结核",
        };

        #endregion Valid Department
    }
}
