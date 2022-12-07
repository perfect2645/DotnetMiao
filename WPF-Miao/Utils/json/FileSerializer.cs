using Newtonsoft.Json;

namespace Utils.json
{
    public static class FileSerializer
    {
        public static T DeSerialize<T>(string json) where T : class
        {
            try
            {
                var tData = JsonConvert.DeserializeObject<T>(json);
                return tData;
            }
            catch(Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
                return null;
            }
        }
    }
}
