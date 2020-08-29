using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Helpers
{
    public static class DictionaryExtend
    {
        public static string GenerateConnectionString(this Dictionary<string, string> connectionPairs)
        {
            return string.Format("server={0};database={1};uid={2};password={3}"
                , connectionPairs.FirstOrDefault(x => x.Key == "server").Value
                , connectionPairs.FirstOrDefault(x => x.Key == "database").Value
                , connectionPairs.FirstOrDefault(x => x.Key == "uid").Value
                , connectionPairs.FirstOrDefault(x => x.Key == "password").Value);
        }
    }
}
