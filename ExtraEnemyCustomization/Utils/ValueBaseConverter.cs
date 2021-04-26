using ExtraEnemyCustomization.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtraEnemyCustomization.Utils
{
    public class ValueBaseConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ValueBase);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return existingValue;

            JToken token = JToken.Load(reader);
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    return new ValueBase(token.ToObject<int>(), ValueMode.Abs);

                case JsonToken.Float:
                    return new ValueBase(token.ToObject<float>(), ValueMode.Abs);

                case JsonToken.StartObject:
                    JObject jobject = JObject.Load(reader);
                    var value = jobject["Value"]?.ToObject<float>() ?? 1.0f;
                    var mode = jobject["Mode"]?.ToObject<ValueMode>() ?? ValueMode.Rel;
                    var fromDef = jobject["FromDefault"]?.ToObject<bool>() ?? true;
                    return new ValueBase(value, mode, fromDef);

                case JsonToken.String:
                    var strValue = ((string)reader.Value).Trim();
                    var fromDefaultFlag = false;

                    if (strValue.EndsWith("of default", StringComparison.OrdinalIgnoreCase))
                    {
                        fromDefaultFlag = true;
                        strValue = strValue[0..^10].TrimEnd();
                    }

                    if (strValue.EndsWith("%"))
                    {
                        if (float.TryParse(strValue[0..^1].TrimEnd(), out var parsedPercent))
                        {
                            return new ValueBase(parsedPercent / 100.0f, ValueMode.Rel, fromDefaultFlag);
                        }
                    }
                    else if (strValue.EqualsAnyIgnoreCase("Unchanged", "Ignore", "Keep", "Original", "KeepOriginal"))
                    {
                        return new ValueBase(1.0f, ValueMode.Rel, false);
                    }
                    else if (float.TryParse(strValue, out var parsedValue))
                    {
                        return new ValueBase(parsedValue, ValueMode.Abs);
                    }
                    Logger.Error($"Cannot parse ValueBase string: {strValue}! Are you sure it's in right format? (returning exstingValue)");
                    return existingValue;

                default:
                    Logger.Error($"ValueBaseJson type: {reader.TokenType} is not implemented! returning existingValue!");
                    return existingValue;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
