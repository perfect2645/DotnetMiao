using HttpProcessor.JsonFactory;
using Newtonsoft.Json;
using System;

namespace WPF_Miao.Platform.DianYiTong.Order
{
    public partial class OrderRequest
    {

        #region Properties

        [JsonProperty("doc_name")]
        public string DocName { get; set; }

        [JsonProperty("hos_name")]
        public string HosName { get; set; }

        [JsonProperty("hos_code")]
        [JsonConverter(typeof(JsonToValue))]
        public long HosCode { get; set; }

        [JsonProperty("dep_name")]
        public string DepName { get; set; }

        [JsonProperty("level_name")]
        public string LevelName { get; set; }

        [JsonProperty("dep_id")]
        [JsonConverter(typeof(JsonToValue))]
        public long DepId { get; set; }

        [JsonProperty("doc_id")]
        [JsonConverter(typeof(JsonToValue))]
        public long DocId { get; set; }

        [JsonProperty("pat_id")]
        public long PatId { get; set; }

        [JsonProperty("schedule_id")]
        public long ScheduleId { get; set; }

        [JsonProperty("jz_card")]
        public string JzCard { get; set; }

        [JsonProperty("sch_date")]
        public DateTimeOffset SchDate { get; set; }

        [JsonProperty("time_type")]
        [JsonConverter(typeof(JsonToValue))]
        public long TimeType { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("ghf")]
        public long Ghf { get; set; }

        [JsonProperty("zlf")]
        public long Zlf { get; set; }

        [JsonProperty("zjf")]
        public long Zjf { get; set; }

        [JsonProperty("jz_start_time")]
        public long JzStartTime { get; set; }

        [JsonProperty("amt")]
        public long Amt { get; set; }

        [JsonProperty("jz_card_type")]
        public long JzCardType { get; set; }

        [JsonProperty("queue_sn_id")]
        public string QueueSnId { get; set; }

        [JsonProperty("wechat_login")]
        public string WechatLogin { get; set; }

        #endregion Properties

        public string ToJson()
        {
            return JsonSerialize<OrderRequest>.ToJson(this);
        }
    }
}
