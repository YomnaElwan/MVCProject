
//using System.Text.Json;

//namespace TequliasRestaurant.Models
//{
//    public static class SessionExtensions
//    {
//        public static void Set<T>(this ISession session, string key, T value)
//        {
//            session.SetString(key, JsonSerializer.Serialize(value));
//        }

//        public static T Get<T>(this ISession session, string key)
//        {
//            var json = session.GetString(key);
//            if (string.IsNullOrEmpty(json))
//            {
//                return default(T);
//            }
//            else
//            {
//                return JsonSerializer.Deserialize<T>(json);
//            }
//        }
//    }
//}




using System.Text.Json;
using System.Text.Json.Serialization;

namespace TequliasRestaurant.Models
{
    public static class SessionExtensions
    {
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve, // Handle circular references
            WriteIndented = true // Optional: Makes JSON more readable
        };

        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value, jsonOptions));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var json = session.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            else
            {
                return JsonSerializer.Deserialize<T>(json, jsonOptions);
            }
        }
    }
}

