using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace MvcExplorer.Models
{
    //Read data from json.
    public static class JsonDataReader
    {
        private static Dictionary<string, object> _jsonDataCollection = new Dictionary<string, object>();

        public static IEnumerable<T> GetFromFile<T>(string fileName)
        {
            if (_jsonDataCollection.ContainsKey(fileName))
            {
                return _jsonDataCollection[fileName] as IEnumerable<T>;
            }

            string path = HttpContext.Current.Server.MapPath("~/Content/" + fileName);
            string jsonText = new StreamReader(path, System.Text.Encoding.Default).ReadToEnd();
            var list = JsonConvert.DeserializeObject<List<T>>(jsonText);
            _jsonDataCollection[fileName] = list;
            return list;
        }
    }
}