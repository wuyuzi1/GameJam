using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    private Dictionary<string, string> _pathDic;
    private Dictionary<string, GameObject> _cacheUIDic;
    private Dictionary<string, GameObject> _openUIDic;

    public override void Init()
    {
        _pathDic = new Dictionary<string, string>();
        _cacheUIDic = new Dictionary<string, GameObject>();
        _openUIDic = new Dictionary<string, GameObject>();
        InitUIPath();
    }

    public void OpenWindow(string uiname)
    {
        if(_openUIDic.ContainsKey(uiname))
        {
            return;
        }
        GameObject window = null;
        if(_cacheUIDic.ContainsKey(uiname))
        {
            window = _cacheUIDic[uiname];
        }
        else
        {
            if(_pathDic.ContainsKey(uiname))
            {
                GameObject go = Resources.Load<GameObject>(_pathDic[uiname]);
                window = GameObject.Instantiate(go);
                _cacheUIDic.Add(uiname, window);
            }
            else
            {
                return;
            }
        }
        window.transform.SetParent(transform);
        window.transform.localPosition = Vector3.zero;
        IWindow iwindow = window.GetComponent<IWindow>();
        if(iwindow!=null)
        {
            iwindow.Open();
        }
        window.SetActive(true);
        _openUIDic.Add(uiname,window);
    }

    public void CloseWindow(string uiname)
    {
        if(!_openUIDic.ContainsKey(uiname))
        {
            return;
        }
        GameObject window = _openUIDic[uiname];
        IWindow iwindow = window.GetComponent<IWindow>();
        if(iwindow!=null)
        {
            iwindow.Close();
        }
        window.SetActive(false);
        _openUIDic.Remove(uiname);
    }

    public GameObject GetWindow(string uiname)
    {
        if(!_openUIDic.ContainsKey(uiname))
        {
            return null;
        }
        return _openUIDic[uiname];
    }

    public void HideWindow(string uiname)
    {
        if(!_openUIDic.ContainsKey(uiname))
        {
            return;
        }
        GameObject window = _openUIDic[uiname];
        window.SetActive(false);
        _openUIDic.Remove(uiname);
    }

    public void ShowWindow(string uiname)
    {
        if (_openUIDic.ContainsKey(uiname) || !_cacheUIDic.ContainsKey(uiname))
        {
            return;
        }
        GameObject window = _cacheUIDic[uiname];
        window.SetActive(true);
        _openUIDic.Add(uiname, window);
    }

    private void InitUIPath()
    {
        _pathDic.Add(Const.InteractionWindow, "UI/Prefab/InteractionWindow");
        _pathDic.Add(Const.GameMainWindow, "UI/Prefab/GameMainWindow");
    }

    public void Clear()
    {
        for(int i=transform.childCount-1; i>=0; i--)
        {
            GameObject childobj = transform.GetChild(i).gameObject;
            Destroy(childobj);
        }
        _cacheUIDic.Clear();
        _openUIDic.Clear();
    }
}
