using Utils.json;

namespace Utils.file
{
    public static class FileReader
    {
        public static string ReadFileAsJson(string path)
        {
            try
            {
                var content = File.ReadAllText(path);
                return content;

            }
            catch(Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
                return string.Empty;
            }
        }

        public static T DeserializeFile<T>(string path) where T : class
        {
            var jsonData = ReadFileAsJson(path);
            return FileSerializer.DeSerialize<T>(jsonData);
        }
    }
}
