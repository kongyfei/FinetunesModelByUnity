using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ʱ��Ϸ������
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
