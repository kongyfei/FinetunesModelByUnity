using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 面板基类
/// </summary>
public abstract class PanelBase : MonoBehaviour
{
    public PanelType type;
    public PanelState state;
    public UseFrequency frequency;
    public string name;
    public bool main;

    public virtual void OnInit()
    {
        Debug.Log("OnInit");
    }

    public virtual void OnShow()
    {
        Debug.Log("OnShow");
    }

    public virtual void OnHide()
    {
        Debug.Log("OnHide");
    }

    protected void Hide()
    {
        PanelManager.Instance.Hide(this);
    }
}

/// <summary>
/// 界面类型
/// </summary>
public enum PanelType
{
    Interface,
    Popups,
}

/// <summary>
/// 使用频率
/// </summary>
public enum UseFrequency
{
    More,
    Less,
}

/// <summary>
/// 界面状态
/// </summary>
public enum PanelState
{
    None,
    Load,
    Show,
    Hide,
}
