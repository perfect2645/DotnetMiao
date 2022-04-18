using HttpProcessor.JsonFactory;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProcessModel.Order;
using System;
using System.ComponentModel;
using System.Globalization;

namespace WPF_Miao.Platform.DianYiTong.Order
{
    internal class OrderSchedule : OrderScheduleModel
    {
        #region Properties

        [Description("amt=0")]
        [JsonProperty("amt")]
        public ushort Amt { get; set; }
        
        [Description("cate_name=13:30-16:00")]
        [JsonProperty("cate_name")]
        public string CateName { get; set; }

        [Description("doc_id=4705")]
        [JsonProperty("doc_id")]
        [JsonConverter(typeof(JsonStringConverter))]
        public string DocId { get; set; }

        [JsonProperty("ghf")]
        public ushort Ghf { get; set; }

        [JsonProperty("src_max")]
        public long SrcMax { get; set; }

        [JsonProperty("src_num")]
        public long SrcNum { get; set; }

        [JsonProperty("zlf")]
        public long Zlf { get; set; }

        [JsonProperty("zjf")]
        public long Zjf { get; set; }

        [JsonProperty("schedule_id")]
        public long ScheduleId { get; set; }

        [JsonProperty("time_type")]
        [JsonConverter(typeof(JsonStringConverter))]
        public long TimeType { get; set; }

        [JsonProperty("sch_date")]
        public DateTimeOffset SchDate { get; set; }

        [JsonProperty("is_datepart")]
        public long IsDatepart { get; set; }

        #endregion Properties
    }

}
