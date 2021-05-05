using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EECustom.Utils
{
    public static class JSON
    {
        private readonly static JsonSerializerOptions _Setting = new JsonSerializerOptions()
        {
            ReadCommentHandling = JsonCommentHandling.Skip
        };

        static JSON()
        {
            _Setting.Converters.Add(new ValueBaseConverter());
            _Setting.Converters.Add(new ColorConverter());
        }

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _Setting);
        }
    }
}
