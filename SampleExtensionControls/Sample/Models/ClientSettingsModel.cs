using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace Sample.Models
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

        public void LoadRequestData(HttpRequestBase request)
        {
            if (!string.Equals(request.HttpMethod, "get", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            IValueProvider data;
            try
            {
                var parameters = request.RequestContext.HttpContext.Request.Params;
                data = new NameValueCollectionValueProvider(parameters, CultureInfo.CurrentCulture);
            }
            catch
            {
                return;
            }

            foreach (var option in Settings)
            {
                var optionName = ToOptionName(option.Key);
                if (!data.ContainsPrefix(optionName)) continue;
                var value = data.GetValue(optionName);
                if (DefaultValues == null)
                {
                    DefaultValues = new Dictionary<string, object>();
                }
                DefaultValues[option.Key] = (string)value.ConvertTo(typeof(string));
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