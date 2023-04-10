using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : PanelBase
{
    public Button button;

    private void OnEnable()
    {
        Debug.Log("OnEnable");

        button.onClick.AddListener(Close);
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnShow()
    {
        base.OnShow();
    }

    public override void OnHide()
    {
        base.OnHide();
    }

    public void Close()
    {
        Hide();
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");

        button.onClick.RemoveListener(Close);
    }
}
