using System;
using System.Collections.Generic;

namespace Baohe.appointment.content
{
    internal class ContentMapper
    {
        private static Dictionary<string, Type> ContentDic { get; }

        static ContentMapper()
        {
            ContentDic = new Dictionary<string, Type>();
            ContentDic.Add("1047063", typeof(JinggangContent)); // 蜀山区井岗中心服务号
            ContentDic.Add("1031364", typeof(JinggangContent)); // 蜀山区西园街道
            ContentDic.Add("1031995", typeof(JinggangContent)); // 蜀山稻香村街道
            ContentDic.Add("1038404", typeof(JinggangContent)); // 包河区同安街道社区卫生服务中心
            ContentDic.Add("1040231", typeof(NangangContent)); // 蜀山区南岗镇卫生院
            ContentDic.Add("1039346", typeof(BaoheContent)); // 包河区包公街道社区服务中心
            ContentDic.Add("1094367", typeof(BaoheContent)); // 包河区万年埠街道
            ContentDic.Add("1047032", typeof(JinggangContent)); // 包河区常青街道社区卫生服务中心
            ContentDic.Add("1094417", typeof(NanjingContent)); // 雨花社区卫生服务中心
            ContentDic.Add("1099108", typeof(YuhuaKaifaqu1099108)); // 雨花经济开发区社区卫生服务中心
            ContentDic.Add("1101211", typeof(BuwenContent)); // 漳州市龙文区步文街道社区卫生服务中心
            ContentDic.Add("1094218", typeof(YuhuaKaifaqu1099108)); // 南京西善桥
            ContentDic.Add("1050391", typeof(YuhuaKaifaqu1099108)); // 南京岱山
            ContentDic.Add("1023044", typeof(WuhanContent));
            ContentDic.Add("1023020", typeof(WuhanContent));
        }

        public static AppointmentContent GetContent(string hospitalId)
        {
            if (ContentDic.ContainsKey(hospitalId))
            {
                var contentType = ContentDic[hospitalId];
                var content = Activator.CreateInstance(contentType);
                return (content as AppointmentContent) ?? new AppointmentContent();
            }

            return new AppointmentContent();
        }
    }
}
