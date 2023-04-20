using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTest : MonoBehaviour
{
    public void Test()
    {
        PanelManager.Instance.ShowLoading();
        RemoteDataPool.Instance.LoadCompanyData(
            ()=>{
                PanelManager.Instance.HideLoading();
                PanelManager.Instance.Show<CompanyShowPanel>();
             }
            );
    }
}
