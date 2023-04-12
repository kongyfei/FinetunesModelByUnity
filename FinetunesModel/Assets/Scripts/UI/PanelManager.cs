using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 面板管理类
/// </summary>
public class PanelManager : MonoSingleton<PanelManager>
{
    /// <summary>
    /// 管理界面的显示顺序
    /// </summary>
    private Stack<PanelBase> panels;

    /// <summary>
    /// 缓存池优化
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

        while (panels.Count > 0 && panels.Peek().type == PanelType.Popups)
        {
            GameObject gameObject = panels.Pop().gameObject;
            Destroy(gameObject);
        }

        if (panels.Count > 0)
        {
            PanelBase hidePanel = panels.Peek();
            hidePanel.gameObject.SetActive(false);
        }

            int popCount = 0;
        bool isPop = false;
        foreach (var item in panels)
        {
            string name = item.gameObject.name;
            if (name == typeName)
            {
                isPop = true;
                break;
            }
            popCount++;
        }

        PanelBase panelBase;
        if (isPop)
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
            panelBase.name = typeName;
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

            if (panels.Count > 0)
            {
                panels.Peek().gameObject.SetActive(true);
            }
        }
        else
        {
            LogExtension.LogFail("面板顺序错误");
        }
    }
}
