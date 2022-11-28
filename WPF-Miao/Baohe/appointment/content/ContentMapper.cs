using System;
using System.Collections.Generic;

namespace Baohe.appointment.content
{
    internal class ContentMapper
    {
        private static Dictionary<string, Type> ContentDic { get; }

        static ContentMapper()
        {
            ContentDic.Add("1094417", typeof(NanjingContent)); // 雨花社区卫生服务中心
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
