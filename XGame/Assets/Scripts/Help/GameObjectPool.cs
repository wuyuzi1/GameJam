using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor;
using UnityEngine;

public class GameObjectPool : Singleton<GameObjectPool>
{
    private Dictionary<string, Stack<GameObject>> _pool;

    public override void Init()
    {
        _pool = new Dictionary<string, Stack<GameObject>>();
    }

    public GameObject GetFromPool(string prefabName)
    {
        Debug.Log("GetFromPool-------------" + prefabName);
        GameObject go = null;
        string pathStr = $"Game/Prefab/Pool/{prefabName}";
        if(!_pool.ContainsKey(prefabName) || _pool[prefabName].Count <=0)
        {
            go = Resources.Load<GameObject>(pathStr);
            go = GameObject.Instantiate(go);
        }
        else
        {
            go = _pool[prefabName].Pop();
        }
        go.name = prefabName;
        go.SetActive(true);
        return go;
    }

    public void PushToPool(GameObject go)
    {
        go.SetActive(false);
        string prefabName = go.name;
        if (!_pool.ContainsKey(prefabName))
        {
            _pool.Add(prefabName, new Stack<GameObject>());
        }
        _pool[prefabName].Push(go);
    }

    public void Clear()
    {
        foreach(Stack<GameObject> itemStack in _pool.Values)
        {
            itemStack.Clear();
        }
    }

    public GameObject GetFromPool(string prefabName,Transform parent)
    {
        GameObject go = null;
        string pathStr = $"Game/Prefab/Pool/{prefabName}";
        if (!_pool.ContainsKey(prefabName) || _pool[prefabName].Count <= 0)
        {
            go = Resources.Load<GameObject>(pathStr);
            go = GameObject.Instantiate(go);
        }
        else
        {
            go = _pool[prefabName].Pop();
        }
        go.transform.SetParent(parent);
        go.transform.localPosition = Vector3.zero;
        go.name = prefabName;
        go.SetActive(true);
        return go;
    }

}
