using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializeDic<TKey,TValue>
{
    [System.Serializable]
    public struct SerializeDicData<Key, Value>
    {
        public Key key;
        public Value value;
    }

    public List<SerializeDicData<TKey, TValue>> serializeDic;
    public Dictionary<TKey, TValue> storeDic;

    public void SetSerializeDic()
    {
        storeDic = new Dictionary<TKey, TValue>();
        for (int i = 0; i < serializeDic.Count; i++)
        {
            storeDic.Add(serializeDic[i].key, serializeDic[i].value);
        }
    }
}
