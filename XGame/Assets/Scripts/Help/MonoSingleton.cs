using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get 
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
                _instance.Init();
            }
            return _instance; 
        }
    }

    public virtual void Awake()
    {
        if(_instance == null)
        {
            _instance = this as T;
        }
    }

    public virtual void Init()
    {

    }
}
