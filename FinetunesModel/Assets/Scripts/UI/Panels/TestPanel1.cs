using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel1 : PanelBase
{
    public Button button;
    public Button button1;

    private void OnEnable()
    {
        Debug.Log("OnEnable");

        button.onClick.AddListener(Close);
        button1.onClick.AddListener(Jump);
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

    private void Jump()
    {
        PanelManager.Instance.Show<TestPanel3>();
    }

    public void Close()
    {
        Hide();
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");

        button.onClick.RemoveListener(Close);
        button1.onClick.RemoveListener(Jump);
    }
}
