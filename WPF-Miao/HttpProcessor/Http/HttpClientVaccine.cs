using Newtonsoft.Json;

namespace HttpProcessor.Http
{
    public static class HttpClientVaccine
    {
        public static async Task<string> HttpGetStringAsync(string url)
        {
            var httpClient = new HttpClient();
            var res = await httpClient.GetAsync(url);

            var resStream = res.Content.ReadAsStream();
            var resString = await res.Content.ReadAsStringAsync();
            return resString;
        }

        public static async Task<object> HttpGetObjectAsync(string url) 
        { 
            var resString = await HttpGetStringAsync(url);
            var jObj = JsonConvert.DeserializeObject<object>(resString);
            return jObj;
        }
    }
}
