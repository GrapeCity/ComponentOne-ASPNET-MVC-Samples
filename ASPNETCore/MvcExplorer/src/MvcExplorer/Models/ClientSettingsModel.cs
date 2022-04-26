using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MvcExplorer.Models
{
    public class ClientSettingsModel
    {
        private string _controlId = "DemoControl";

        public ClientSettingsModel(string controlId)
        {
            _controlId = controlId;
        }

        public ClientSettingsModel()
        {
        }

        public virtual string ControlId
        {
            get { return _controlId; }
        }

        public IDictionary<string, object[]> Settings { get; set; }

        public IDictionary<string, object> DefaultValues { get; set; }

        public void LoadRequestData(HttpRequest request, bool camelCase = true)
        {
            if (!string.Equals(request.Method, "get", System.StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var query = request.Query;
            foreach (var option in Settings)
            {
                var optionName = camelCase ? ToOptionName(option.Key) : option.Key;
                if (!query.ContainsKey(optionName)) continue;
                var value = query[optionName].ToString();
                if (DefaultValues == null)
                {
                    DefaultValues = new Dictionary<string, object>();
                }
                DefaultValues[option.Key] = value;
            }
        }

        public static string ToOptionName(string name)
        {
            return name == null ? null : name.Replace(" ", "").ToLower();
        }

        public T GetEnum<T>(string key, T defaultValue, System.Func<string, string> converter = null) where T : struct
        {
            T result = defaultValue;
            if (DefaultValues != null && DefaultValues.ContainsKey(key))
            {
                var value = DefaultValues[key];
                if (value is string)
                {
                    var content = (string)value;
                    if (converter != null)
                    {
                        content = converter(content);
                    }
                    System.Enum.TryParse(content, true, out result);
                }
                else
                {
                    try
                    {
                        result = (T)value;
                    }
                    catch { }
                }
            }
            return result;
        }

        public bool GetBool(string key, bool defaultValue)
        {
            bool result = defaultValue;
            if (DefaultValues != null && DefaultValues.ContainsKey(key))
            {
                var value = DefaultValues[key];
                if (value is string)
                {
                    bool.TryParse((string)value, out result);
                }
                else
                {
                    try
                    {
                        result = (bool)value;
                    }
                    catch { }
                }
            }
            return result;
        }

        public float GetFloat(string key, float defaultValue)
        {
            float result = defaultValue;
            if (DefaultValues != null && DefaultValues.ContainsKey(key))
            {
                var value = DefaultValues[key];
                if (value is string)
                {
                    float.TryParse((string)value, out result);
                }
                else
                {
                    try
                    {
                        result = (float)value;
                    }
                    catch { }
                }
            }
            return result;
        }
    }
}