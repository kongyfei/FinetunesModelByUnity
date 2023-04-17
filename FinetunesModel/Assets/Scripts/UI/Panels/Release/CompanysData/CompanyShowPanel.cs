using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��˾����
/// </summary>
public class CompanyShowPanel : PanelBase
{
    //�Ż�
    //1. Ԥ�������ݼ���


    [Header("�����б�")]
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
