using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������
/// </summary>
public class PanelManager : MonoSingleton<PanelManager>
{
    /// <summary>
    /// ����������ʾ˳��
    /// </summary>
    private Stack<PanelBase> panels;

    /// <summary>
    /// ������Ż�
    /// </summary>
    private Dictionary<string, GameObject> prefabPool;
    private Dictionary<string, GameObject> panelPool;

    public Transform UIParent;
    public string rootPath;

    protected override void Awake()
    {
        base.Awake();

        panels = new Stack<PanelBase>();
        prefabPool = new Dictionary<string, GameObject>();
        panelPool = new Dictionary<string, GameObject>();
    }

    public void Show<T>()
    {
        string typeName = typeof(T).ToString();

        int popCount = 0;
        foreach (var item in panels)
        {
            string name = item.gameObject.name;
            if (name == typeName)
            {
                break;
            }
            popCount++;
        }

        PanelBase panelBase;
        if (popCount > 0)
        {
            for (int i = 0; i < popCount; i++)
            {
                GameObject gameObject = panels.Pop().gameObject;
                Destroy(gameObject);
            }

            panelBase = panels.Peek();
            panelBase.gameObject.SetActive(true);
        }
        else
        {
            GameObject prefab;
            if (!prefabPool.ContainsKey(typeName))
            {
                string prefabPath = rootPath + typeName;
                prefab = Resources.Load(prefabPath) as GameObject;
                prefabPool.Add(typeName, prefab);
            }
            else
            {
                prefab = prefabPool[typeName];
            }

            GameObject panel = Instantiate(prefab, UIParent, false);
            panel.name = typeName;
            panelBase = panel.GetComponent<PanelBase>();
            panels.Push(panelBase);
            panelBase.state = PanelState.Load;
        }
        panelBase.OnInit();
        panelBase.OnShow();
        panelBase.state = PanelState.Show;
    }

    public void Hide(PanelBase panelBase)
    {
        PanelBase curPanel = panels.Peek();
        if (curPanel.gameObject.name == panelBase.gameObject.name)
        {
            panelBase.OnHide();
            panels.Pop();
            Destroy(panelBase.gameObject);
        }
        else
        {
            LogExtension.LogFail("���˳�����");
        }
    }
}
