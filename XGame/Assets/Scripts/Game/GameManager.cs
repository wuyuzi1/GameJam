using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private void OnEnable()
    {
        
    }

    private void Start()
    {
        UIManager.Instance.OpenWindow(Const.GameMainWindow);
    }

    private void Update()
    {
        TimerManager.Instance.UpdateTimer(Time.deltaTime);
    }

  
}
