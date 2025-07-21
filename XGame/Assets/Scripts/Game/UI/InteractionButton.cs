using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractionButton : MonoBehaviour
{
    private TextMeshProUGUI _buttonText;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();    
    }

    public void SetText(string text)
    {
        _buttonText.text = text;
    }

    public void SetButtonAction(Action<Transform> callback, Transform trans)
    {
        _button.onClick.AddListener(()=>
        {
            callback.Invoke(trans);
            UIManager.Instance.CloseWindow(Const.InteractionWindow);
            EventCenter.Instance.TriggerEvent("SetPlayerNeedCheck", false);
        });
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }
}
