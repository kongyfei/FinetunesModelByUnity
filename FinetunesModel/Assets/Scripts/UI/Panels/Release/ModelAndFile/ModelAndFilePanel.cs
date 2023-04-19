using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 模型与训练文件界面
/// </summary>
public class ModelAndFilePanel : PanelBase
{
    public Button close;
    public override void OnInit()
    {
        base.OnInit();
    }

    private void OnEnable()
    {
        close.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        close.onClick.RemoveListener(Close);
    }

    public override void OnShow()
    {
        base.OnShow();
    }

    public override void OnHide()
    {
        base.OnHide();
    }

    private void Close()
    {
        Hide();
    }
}
