using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 面板管理类
/// </summary>
public class PanelManager : MonoSingleton<PanelManager>
{
    private Stack<PanelBase> panels;

    protected override void Awake()
    {
        base.Awake();

        panels = new Stack<PanelBase>();
    }

    public void Show<T>()
    {
        
    }
}
