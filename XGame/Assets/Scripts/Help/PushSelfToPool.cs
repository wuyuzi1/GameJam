using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSelfToPool : MonoBehaviour
{
    public void OnEnable()
    {
        Invoke("PushSelf", 5f);
    }

    private void PushSelf()
    {
        GameObjectPool.Instance.PushToPool(gameObject);
    }
}
