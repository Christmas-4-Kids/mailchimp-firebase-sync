using Newtonsoft.Json;
using System;

namespace mailchimp_firebase_sync.Converters
{
    public class BooleanJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null || reader.Value.ToString() == string.Empty) return false;
            switch (reader.Value.ToString().ToLower().Trim())
            {
                case "true":
                case "yes":
                case "y":
                case "1":
                    return true;
                case "false":
                case "no":
                case "n":
                case "0":
                    return false;
                default:
                    return true;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }
    }
}
