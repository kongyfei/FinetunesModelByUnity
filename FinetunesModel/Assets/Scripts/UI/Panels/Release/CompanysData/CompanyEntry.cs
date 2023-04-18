using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyEntry : LoopEntry
{
    public Image companyIcon;
    public Text companyName;
    public Button jumpBtn;

    private void OnEnable()
    {
        jumpBtn.onClick.AddListener(JumpPanel);
    }

    private void OnDisable()
    {
        jumpBtn.onClick.RemoveListener(JumpPanel);
    }

    private void JumpPanel()
    {
        
    }

    public void Refresh(CompanyEntryData data)
    {
        companyIcon.sprite = ResourceManager.Instance.LoadSprite(data.name);
        companyName.text = data.name;
    }
}

public class CompanyEntryData
{
    public string name;

    public CompanyEntryData(string name)
    {
        this.name = name;
    }
}
