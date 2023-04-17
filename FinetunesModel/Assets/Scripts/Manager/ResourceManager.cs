using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源管理
/// </summary>
public class ResourceManager : MonoSingleton<ResourceManager>
{
    [Header("公司图标路径")]
    public string companysIconPath;

    public Sprite LoadSprite(string path)
    {
        Sprite sprite = Resources.Load<Sprite>(companysIconPath + path);
        return sprite;
    }
}
