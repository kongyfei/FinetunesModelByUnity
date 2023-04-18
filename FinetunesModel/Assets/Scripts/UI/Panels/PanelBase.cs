using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������
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
/// ��������
/// </summary>
public enum PanelType
{
    Interface,
    Popups,
}

/// <summary>
/// ʹ��Ƶ��
/// </summary>
public enum UseFrequency
{
    More,
    Less,
}

/// <summary>
/// ����״̬
/// </summary>
public enum PanelState
{
    None,
    Load,
    Show,
    Hide,
}
