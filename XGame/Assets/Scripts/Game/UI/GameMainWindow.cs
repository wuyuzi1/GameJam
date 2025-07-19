using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class GameMainWindow : MonoBehaviour, IWindow
{
    private Button _exitButton;

    private void Awake()
    {
        _exitButton = transform.GetChild(0).GetComponent<Button>();
    }

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(ReturnToLoading);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(ReturnToLoading);
    }

    private void ReturnToLoading()
    {
        SceneLoader.Instance.LoadScene("SignScene");
    }

    public void Close()
    {

    }

    public void Open()
    {

    }
}
