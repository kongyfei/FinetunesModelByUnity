using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDataPool : DataPoolBase
{
    private Dictionary<string, DataBase> dataPool;

    public MyDataPool(int capacity)
    {
        dataPool = new Dictionary<string, DataBase>(capacity);
    }
}
