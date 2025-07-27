using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private FishingSystem fishingSystem;
    private void OnEnable()
    {
        
    }

    private void Start()
    {
        UIManager.Instance.OpenWindow(Const.GameMainWindow);
        fishingSystem = new FishingSystem();
    }

    private void Update()
    {
        TimerManager.Instance.UpdateTimer(Time.deltaTime);
    }

    public void ChangePlayerCameraFellow(Transform target)
    {
        GameObject cameraGo = GameObject.FindGameObjectWithTag("PlayerFellowCamera");
        CinemachineVirtualCamera virtualCamera = cameraGo.GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = target;
    }

  
}
