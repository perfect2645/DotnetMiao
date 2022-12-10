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

        public static string Serialize<T>(T obj) where T : class
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                return json;
            }
            catch (Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
                return null;
            }
        }
    }
}
