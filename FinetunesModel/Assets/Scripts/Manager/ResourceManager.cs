using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Դ����
/// </summary>
public class ResourceManager : MonoSingleton<ResourceManager>
{
    [Header("��˾ͼ��·��")]
    public string companysIconPath;

    public Sprite LoadSprite(string path)
    {
        Sprite sprite = Resources.Load<Sprite>(companysIconPath + path);
        return sprite;
    }
}
