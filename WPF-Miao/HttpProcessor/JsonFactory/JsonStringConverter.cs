using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HttpProcessor.JsonFactory
{
    public class JsonStringConverter : JsonConverter
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(long) || typeToConvert == typeof(long?);
    }
}
