using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtraEnemyCustomization.Utils
{
    public static class JSON
    {
        private readonly static JsonSerializerSettings _Setting = new JsonSerializerSettings()
        {
            Converters = new JsonConverter[]
            {
                new ValueBaseConverter(),
                new ColorConverter()
            }
        };

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _Setting);
        }
    }
}
