using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWindow : MonoBehaviour,IWindow
{
    public void Close()
    {
        DestoryAll();
    }

    public void DestoryAll()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObjectPool.Instance.PushToPool(transform.GetChild(i).gameObject);
        }
    }

    public void Open()
    {

    }
}
