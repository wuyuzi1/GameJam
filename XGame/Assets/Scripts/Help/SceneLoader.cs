using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoSingleton<SceneLoader>
{
    private Animator _fadeAnim;
    private GameObject _fadeGo;

    private new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        _fadeGo = transform.Find("Fade").gameObject;
        _fadeGo.SetActive(false);
        _fadeAnim = _fadeGo.GetComponent<Animator>();
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneFade(sceneName));
    }

    private IEnumerator LoadSceneFade(string sceneName)
    {
        _fadeGo.SetActive(true);
        _fadeAnim.Play("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(3f);
        AudioManager.Instance.SetBGM($"bgm_{sceneName}");
        _fadeGo.SetActive(false);
    }
    
}
