using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.stringBuilder
{
    public static class UrlConverter
    {
        public static Dictionary<string, object> QueryToDic(string query)
        {
            var result = new Dictionary<string, object>();
            if (string.IsNullOrEmpty(query))
            {
                return result;
            }

            try
            {
                var paramList = query.Split('&').ToList();
                foreach (var param in paramList)
                {
                    var keyValList = param.Split('=');
                    if (keyValList.Length != 2)
                    {
                        continue;
                    }
                    result.AddOrUpdate(keyValList[0], keyValList[1]);
                }

                return result;
            }
            catch(Exception ex)
            {
                Logging.GLog.Logger.Error($"UrlConverter::QueryToDic failed.{ex.Message}");
                return result;
            }
        }
    }
}
