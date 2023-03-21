using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

/// <summary>
/// �Զ���dropdown
/// </summary>
public class MyDropDown : Dropdown
{
    /// <summary>
    /// ��ʼ����������
    /// </summary>
    /// <param name="datas">������������</param>
    /// <param name="onValueChange">��ѡ��ʱ�Ļص�</param>
    public void Init(List<DrapDownItem> datas, UnityAction<int> onValueChange = null)
    {
        //�������
        this.ClearOptions();
        //�������
        for (int i = 0; i < datas.Count; i++)
        {
            OptionData data = new OptionData();
            if (!string.IsNullOrEmpty(datas[i].text) && datas[i].sprite != null)
            {
                data.text = datas[i].text;
                data.image = datas[i].sprite;

                if (i == 0)
                {
                    this.captionText.text = datas[i].text;
                }
            }
            else if (!string.IsNullOrEmpty(datas[i].text))
            {
                data.text = datas[i].text;

                if (i == 0)
                {
                    this.captionText.text = datas[i].text;
                }
            }
            else
            {
                LogExtension.LogFail("���������п�ֵ");
            }

            this.options.Add(data);
        }

        if (onValueChange != null)
        {
            this.onValueChanged.AddListener(onValueChange);
        }
    }

    public void Test()
    {
        DrapDownItem item = new DrapDownItem("4");
        DrapDownItem item1 = new DrapDownItem("3");
        Init(new List<DrapDownItem>() { item , item1 }, (index) => { LogExtension.LogSuccess("���Գɹ�" + index); });
    }
}

public class DrapDownItem
{
    public string text;
    public Sprite sprite;

    public DrapDownItem(string text)
    {
        this.text = text;
    }

    public DrapDownItem(string text, Sprite sprite)
    {
        this.text = text;
        this.sprite = sprite;
    }
        
}
