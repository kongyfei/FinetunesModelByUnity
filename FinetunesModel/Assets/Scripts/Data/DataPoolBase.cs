using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数据池基类
/// 作用：管理数据
/// </summary>
public abstract class DataPoolBase
{
    public abstract void Init();

    public abstract void Save();
}
