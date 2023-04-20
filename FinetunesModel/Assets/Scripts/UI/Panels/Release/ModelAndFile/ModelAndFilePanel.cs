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
    public LoopVerticalScrollRect scrollRect;
    public GameObject modelEntry;
    public MyDropDown modelDropDown;
    public Button addModel;

    public List<ModelEntryData> modelEntryDatas;

    public override void OnInit()
    {
        base.OnInit();

        scrollRect.Init(4, modelEntry);

        for (int i = 0; i < scrollRect.EntryList.Count; i++)
        {
            scrollRect.EntryList[i].OnIndexChanged = RefreshItem;
        }
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

        scrollRect.UpdateData(modelEntryDatas.Count);
    }

    private void RefreshItem(LoopEntry entry, int index)
    {
        ModelEntry tempEntry = entry as ModelEntry;
        tempEntry.Refresh(modelEntryDatas[index]);
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
