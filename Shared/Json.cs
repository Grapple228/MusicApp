using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace Shared;
public static class Json
{
    public static string InstanceToString<T>(T instance)
    {
        return JsonConvert.SerializeObject(instance);
    }
    public static Dictionary<string, object>? StringToDictionary (string json)
    {
        return JsonConvert.DeserializeObject<Dictionary<String, Object>>(json);
    }
    public static T? StringToInstance<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
    public static byte[] InstanceToBytes<T>(T instance)
    {
        MemoryStream ms = new MemoryStream();
        using (BsonDataWriter writer = new BsonDataWriter(ms))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(writer, instance);
        }
        return ms.ToArray();
    }
    public static T? BytesToInstance<T>(byte[] bytes)
    {
        T? instance;
        MemoryStream ms = new MemoryStream(bytes);
        
        using (BsonDataReader reader = new BsonDataReader(ms))
        {
            JsonSerializer serializer = new JsonSerializer();
            instance = serializer.Deserialize<T>(reader);
        }

        return instance;
    }
}