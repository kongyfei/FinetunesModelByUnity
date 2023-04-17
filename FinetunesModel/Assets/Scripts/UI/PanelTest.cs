using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTest : MonoBehaviour
{
    public void Test()
    {
        RemoteDataPool.Instance.LoadCompanyData(
            ()=>{
                PanelManager.Instance.Show<CompanyShowPanel>();
             }
            );
    }
}
