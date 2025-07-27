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
    private Button _quitFishingButton;

    private void Awake()
    {
        _exitButton = transform.GetChild(0).GetComponent<Button>(); 
        _startFishingButton = transform.Find("StartFishingButton").GetComponent<Button>();
        _quitFishingButton = transform.Find("CancelFishingButton").GetComponent<Button>();
    }

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(ReturnToLoading);
        _startFishingButton.onClick.AddListener(ActiveBuoy);
        _quitFishingButton.onClick.AddListener (QuitFishingMode);
        EventCenter.Instance.AddListener("ShowFishingStartButton", ShowFishingStartButton);
        EventCenter.Instance.AddListener("HideAllFishingButton", HideAllFishingButton);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(ReturnToLoading);
        _startFishingButton.onClick.RemoveListener(ActiveBuoy);
        _quitFishingButton.onClick.RemoveListener(QuitFishingMode);
        EventCenter.Instance.RemoveListener("ShowFishingStartButton", ShowFishingStartButton);
        EventCenter.Instance.RemoveListener("HideAllFishingButton", HideAllFishingButton);
    }

    private void ReturnToLoading()
    {
        SceneLoader.Instance.LoadScene("SignScene");
    }

    private void ActiveBuoy()
    {
        EventCenter.Instance.TriggerEvent("ActiveBuoy",true);
        EventCenter.Instance.TriggerEvent("SetPlayerInputMapActivate", false);
        AudioManager.Instance.PlayEffectAudio("sound_dropIteminwater");
        _startFishingButton.gameObject.SetActive(false);
        _quitFishingButton.gameObject.SetActive(true);
    }

    private void QuitFishingMode()
    {
        _quitFishingButton.gameObject.SetActive(false);
        FishingSystem.Instance.QuitFishing();
    }

    private void ShowFishingStartButton(object[] args)
    {
        _startFishingButton.gameObject.SetActive((bool)args[0]);
    }

    private void HideAllFishingButton(object[] args)
    {
        _startFishingButton.gameObject.SetActive(false);
        _quitFishingButton.gameObject.SetActive(false);
    }

    public void Close()
    {

    }

    public void Open()
    {

    }
}
