using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LoopEntry : MonoBehaviour 
{
    public Action<LoopEntry, int> OnIndexChanged = delegate { };
	public int Index { get; private set; }
    public bool IsEmpty { get { return !this.gameObject.activeSelf; } }

    public LoopEntry()
    {
    }

    public void UpdateIndex(int index)
    {
		Index = index;

        OnIndexChanged(this, index);
    }
}

