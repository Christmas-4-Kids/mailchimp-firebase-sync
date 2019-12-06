using Newtonsoft.Json.Linq;
using System.Diagnostics.Contracts;
using System.Linq;

namespace mailchimp_firebase_sync.Extensions
{
    public static class JTokenExtensions
    {
        // Recursively converts a JObject with PascalCase names to camelCase
        public static JObject ToCamelCase(this JObject original)
        {
            var newObj = new JObject();
            foreach (var property in original.Properties())
            {
                var newPropertyName = property.Name.ToCamelCaseString();
                newObj[newPropertyName] = property.Value.ToCamelCaseJToken();
            }

            return newObj;
        }

        // Recursively converts a JToken with PascalCase names to camelCase
        public static JToken ToCamelCaseJToken(this JToken original)
        {
            return original.Type switch
            {
                JTokenType.Object => ((JObject)original).ToCamelCase(),
                JTokenType.Array => new JArray(((JArray)original).Select(x => x.ToCamelCaseJToken())),
                _ => original.DeepClone(),
            };
        }

        // Convert a string to camelCase
        public static string ToCamelCaseString(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return char.ToLowerInvariant(str[0]) + str.Substring(1);
            }

            return str;
        }
    }
}
