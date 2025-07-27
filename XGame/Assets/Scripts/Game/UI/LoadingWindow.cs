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
        _startButton = transform.Find("StartButton").GetComponent<Button>();
        _optionButton = transform.Find("OptionButton").GetComponent<Button>();
        _quitButton = transform.Find("QuitButton").GetComponent<Button>();
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
