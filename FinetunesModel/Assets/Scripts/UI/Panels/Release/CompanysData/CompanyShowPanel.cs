using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 公司界面
/// </summary>
public class CompanyShowPanel : PanelBase
{
    //优化
    //1. 预处理数据加载


    [Header("滚动列表")]
    public LoopVerticalScrollRect scrollRect;
    public GameObject entryPrefab;
    public List<CompanyEntryData> companyDatas;

    public override void OnInit()
    {
        base.OnInit();

        companyDatas = LocalDataPool.Instance.companyEntryDatas;

        scrollRect.Init(4, entryPrefab);
        for (int i = 0; i < scrollRect.EntryList.Count; i++)
        {
            scrollRect.EntryList[i].OnIndexChanged += RefreshItem;
        }
    }

    public override void OnShow()
    {
        base.OnShow();


    }

    public override void OnHide()
    {
        base.OnHide();
    }

    private void RefreshItem(LoopEntry entry, int index)
    {
        CompanyEntry companyEntry = entry as CompanyEntry;
        companyEntry.Refresh(companyDatas[index]);
    }
}
