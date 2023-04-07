using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ãæ°å»ùÀà
/// </summary>
public abstract class PanelBase : MonoBehaviour
{
    public PanelType type;
    public PanelState state;

    public virtual void Init()
    {
        
    }

    public virtual void Show()
    {
        
    }

    public virtual void Hide()
    {
        
    }
}

public enum PanelType
{
    Interface,
    Popups,
}

public enum PanelState
{
    None,
    Load,
    Show,
    Hide,
}
