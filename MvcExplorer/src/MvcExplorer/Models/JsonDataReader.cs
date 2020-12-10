using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MvcExplorer.Models
{
    //Read data from json.
    public static class JsonDataReader
    {
        private static Dictionary<string, object> _jsonDataCollection = new Dictionary<string, object>();

        public static IEnumerable<T> GetFromAssembly<T>(string fileName)
        {
            if (_jsonDataCollection.ContainsKey(fileName))
            {
                return _jsonDataCollection[fileName] as IEnumerable<T>;
            }

            string jsonText = new StreamReader(GetJsonStream(fileName)).ReadToEnd();
            var list = JsonConvert.DeserializeObject<List<T>>(jsonText);
            _jsonDataCollection[fileName] = list;
            return list;
        }

        public static Stream GetJsonStream(string name)
        {
            var assembly = typeof(JsonDataReader).GetTypeInfo().Assembly;
            var ress = assembly.GetManifestResourceNames();
            var res = assembly.GetManifestResourceNames().Where(resName => resName.Contains(name)).ToList();
            if (res.Count == 0)
            {
                throw new ArgumentException(string.Format("Cannot find {0} from assembly resources.", name));
            }
            return assembly.GetManifestResourceStream(res[0]);
        }
    }
}
