using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWindow : MonoBehaviour
{
    private Button _startButton;
    private Button _optionButton;
    private Button _quitButton;

    private void Awake()
    {
        _startButton = transform.GetChild(0).GetComponent<Button>();
        _optionButton = transform.GetChild(1).GetComponent<Button>();
        _quitButton = transform.GetChild(2).GetComponent<Button>();
    }

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
        _optionButton.onClick.AddListener(SetOption);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartGame);
        _optionButton.onClick.RemoveListener(SetOption);
        _quitButton.onClick.RemoveListener(QuitGame);
    }

    private void StartGame()
    {
        SceneLoader.Instance.LoadScene("GameScene");
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void SetOption()
    {

    }
}
