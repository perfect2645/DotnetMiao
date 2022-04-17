using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProcessModel.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Miao.Platform.DianYiTong.Order
{
    internal class OrderSchedule : OrderScheduleModel
    {
        #region Properties

        [Description("amt=0")]
        public ushort Amt { get; set; }
        
        [Description("cate_name=13:30-16:00")]
        public string Cate_Name { get; set; }

        [Description("doc_id=4705")]
        public string Doc_Id { get; set; }

        [Description("doc_id=4705")]
        public ushort Ghf { get; set; }


        #endregion Properties
    }

    public partial class Datum
    {
        [JsonProperty("schedule_id")]
        public long ScheduleId { get; set; }

        [JsonProperty("time_type")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TimeType { get; set; }

        [JsonProperty("sch_date")]
        public DateTimeOffset SchDate { get; set; }

        [JsonProperty("src_max")]
        public long SrcMax { get; set; }

        [JsonProperty("src_num")]
        public long SrcNum { get; set; }

        [JsonProperty("cate_name")]
        public string CateName { get; set; }

        [JsonProperty("ghf")]
        public long Ghf { get; set; }

        [JsonProperty("zlf")]
        public long Zlf { get; set; }

        [JsonProperty("zjf")]
        public long Zjf { get; set; }

        [JsonProperty("amt")]
        public long Amt { get; set; }

        [JsonProperty("doc_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long DocId { get; set; }

        [JsonProperty("is_datepart")]
        public long IsDatepart { get; set; }
    }

    public partial class Sch
    {
        public static Sch FromJson(string json) => JsonConvert.DeserializeObject<Sch>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Sch self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
