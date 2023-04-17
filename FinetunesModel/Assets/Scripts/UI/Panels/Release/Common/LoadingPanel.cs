using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// º”‘ÿ÷–µØ¥∞
/// </summary>
public class LoadingPanel : PanelBase
{
    public Image loadingIcon;
    public float rotateSpeed = 100f;

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnShow()
    {
        base.OnShow();
    }

    private void Update()
    {
        loadingIcon.transform.Rotate(Vector3.forward, Time.deltaTime * rotateSpeed);
    }

    public override void OnHide()
    {
        base.OnHide();
    }


}
