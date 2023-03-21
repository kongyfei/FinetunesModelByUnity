using System.Text;
using LitJson;
using System.Collections.Generic;

/// <summary>
/// ����ת��
/// </summary>
public class MyConvert
{
    /// <summary>
    /// json�ַ���ת�ֽ�
    /// </summary>
    /// <param name="json">json�ַ���</param>
    /// <returns>�ֽ�����</returns>
    public static byte[] JsonToBytes(string json)
    {
        return  Encoding.UTF8.GetBytes(json);
    }

    /// <summary>
    /// ���л���jsonת�������ݶ���
    /// </summary>
    public static T Serialize<T>(string json)
    {
        return JsonMapper.ToObject<T>(json);
    }

    /// <summary>
    /// �����У����ݶ���ת����json
    /// </summary>
    public static string Deserialize<T>(T t)
    {
        return JsonMapper.ToJson(t);
    }
}
