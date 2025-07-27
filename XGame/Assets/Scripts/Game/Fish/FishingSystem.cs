using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSystem : MonoSingleton<FishingSystem>
{
    private int _fishID;
    private int _fishLevel;
    private FishSO _fishSO;
    private int _curRoundCircle;
    private int _totalRound;
    private int _curRound;
    private int _innerCircleWeight;
    private int _outerCircleWeight;

    private new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _innerCircleWeight = 1;
        _outerCircleWeight = 1;
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddListener("StartFishingInteraction", StartFishingInteraction);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("StartFishingInteraction", StartFishingInteraction);
    }

    private void StartFishingInteraction(object[] args)
    {
        _fishID = (int) args[0];
        _fishSO = (FishSO) args[1];
        _fishLevel = (int) args[2];
        _curRound = 0;
        switch(_fishLevel)
        {
            case 1:
                _totalRound = 3;
            break;
            case 2:
                _totalRound = 4;
            break;
            case 3:
                _totalRound = 5;
            break;
        }
        PlayEffect();
    }

    private void PlayEffect()
    {
        _curRoundCircle = Random.Range(1,5);
        EventCenter.Instance.TriggerEvent("SetYellowCircle",_curRoundCircle);
    }

    private void FishingEnd()
    {
        _curRound = 0;
        _totalRound = 0;
        UIManager.Instance.CloseWindow(Const.FishingInteractionWindow);
    }

}
