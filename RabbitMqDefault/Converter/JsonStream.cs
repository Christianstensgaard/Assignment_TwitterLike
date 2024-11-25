using System.Text.Json;

namespace RabbitMqDefault.Converter;
public class JsonStream<T>
{
    public T? Data { get; private set; }

    public static T? ToJson(byte[] stream)
    {
        string rawJson = System.Text.Encoding.UTF8.GetString(stream);
        T data = JsonSerializer.Deserialize<T>(rawJson);
        return data;
    }

    public static byte[] ToStream(T json)
    {
        string raw = JsonSerializer.Serialize<T>(json);
        byte[] stream = System.Text.Encoding.UTF8.GetBytes(raw);
        return stream;
    }
}