using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace EECustom.Utils
{
    public class ColorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Color);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    JObject jobject = JObject.Load(reader);
                    var r = jobject["r"]?.ToObject<float>() ?? 1.0f;
                    var g = jobject["g"]?.ToObject<float>() ?? 1.0f;
                    var b = jobject["b"]?.ToObject<float>() ?? 1.0f;
                    var a = jobject["a"]?.ToObject<float>() ?? 1.0f;
                    return new Color(r, g, b, a);

                case JsonToken.String:
                    var strValue = ((string)reader.Value).Trim();
                    if(ColorUtility.TryParseHtmlString(strValue, out var color))
                    {
                        return color;
                    }
                    Logger.Error($"Cannot parse color string: {strValue}! Are you sure it's in right format? (returning exstingValue)");
                    return existingValue;

                default:
                    Logger.Error($"ColorJson type: {reader.TokenType} is not implemented! returning existingValue!");
                    return existingValue;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
