using HttpProcessor.JsonFactory;
using System.Text;
using System.Text.Json;

namespace HttpProcessor.Client
{
    public abstract class HttpClientContentBase
    {
        private readonly Dictionary<string, object> _contents = new Dictionary<string, object>();

        public StringContent GetJsonContent()
        {
            var jsonString = JsonSerializer.Serialize(_contents, JsonEncoder.JsonOption);
            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }
    }
}
