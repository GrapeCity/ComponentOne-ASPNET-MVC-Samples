using C1.JsonNet;
using C1.JsonNet.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageCombo.Controls
{
    public class Base64StringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        protected override void WriteJson(JsonWriter writer, object value)
        {
            if (value == null)
            {
                writer.WriteValue(null);
                return;
            }
            if (value is byte[])
            {
                byte[] rawImage = (byte[])value, strippedImage = new byte[0];

                //Strip OLE header.
                if ((rawImage[0] == 21) && (rawImage[1] == 28))
                {
                    strippedImage = new byte[rawImage.Length - 78];
                    System.Buffer.BlockCopy(rawImage, 78, strippedImage, 0, rawImage.Length - 78);
                }

                writer.WriteValue(Convert.ToBase64String(strippedImage));
            }
        }
    }
}