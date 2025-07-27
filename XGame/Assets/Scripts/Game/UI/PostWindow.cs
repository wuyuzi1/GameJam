using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PostWindow : MonoBehaviour,IWindow
{
    private TextMeshProUGUI _title;
    private Button _button;
    public void Close()
    {
        
    }

    public void Open()
    {
        
    }

    private void Awake()
    {
        _title = GetComponentInChildren<TextMeshProUGUI>();
        _button = GetComponentInChildren<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(CloseSelf);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(CloseSelf);
    }

    public void SetPost(string itemname)
    {
        _title.text = $"恭喜开启宝获得了:{itemname}";
    }

    private void CloseSelf()
    {
        UIManager.Instance.CloseWindow(Const.PostWindow);
    }
}
