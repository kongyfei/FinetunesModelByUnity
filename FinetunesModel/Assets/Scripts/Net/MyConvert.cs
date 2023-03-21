using System.Text;
using LitJson;
using System.Collections.Generic;

/// <summary>
/// 数据转换
/// </summary>
public class MyConvert
{
    /// <summary>
    /// json字符串转字节
    /// </summary>
    /// <param name="json">json字符串</param>
    /// <returns>字节数组</returns>
    public static byte[] JsonToBytes(string json)
    {
        return  Encoding.UTF8.GetBytes(json);
    }

    /// <summary>
    /// 序列化：json转换成数据对象
    /// </summary>
    public static T Serialize<T>(string json)
    {
        return JsonMapper.ToObject<T>(json);
    }

    /// <summary>
    /// 反序列：数据对象转换成json
    /// </summary>
    public static string Deserialize<T>(T t)
    {
        return JsonMapper.ToJson(t);
    }
}
