using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 临时游戏管理器
/// </summary>
public class GameManager : MonoBehaviour
{
    private MyDataPool pool;

    private void Awake()
    {
        pool = new MyDataPool(10);
        //MyNet.instance.StartAsycnNet(MyUrl.GET_ALL_COMPANYS, "GET", );
    }
}
