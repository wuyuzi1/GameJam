using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class GameMainWindow : MonoBehaviour, IWindow
{
    private Button _exitButton;
    private Button _startFishingButton;

    private void Awake()
    {
        _exitButton = transform.GetChild(0).GetComponent<Button>(); 
        _startFishingButton = transform.Find("StartFishingButton").GetComponent<Button>();
    }

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(ReturnToLoading);
        _startFishingButton.onClick.AddListener(ActiveBuoy);
        EventCenter.Instance.AddListener("ShowFishingStartButton", ShowFishingStartButton);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(ReturnToLoading);
        _startFishingButton.onClick.RemoveListener(ActiveBuoy);
        EventCenter.Instance.RemoveListener("ShowFishingStartButton", ShowFishingStartButton);
    }

    private void ReturnToLoading()
    {
        SceneLoader.Instance.LoadScene("SignScene");
    }

    private void ActiveBuoy()
    {
        EventCenter.Instance.TriggerEvent("ActiveBuoy",true);
        _startFishingButton.gameObject.SetActive(false);
    }

    private void ShowFishingStartButton(object[] args)
    {
        _startFishingButton.gameObject.SetActive((bool)args[0]);
    }

    public void Close()
    {

    }

    public void Open()
    {

    }
}
