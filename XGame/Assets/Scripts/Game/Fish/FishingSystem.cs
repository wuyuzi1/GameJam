using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSystem: Singleton<FishingSystem>
{
    private int _fishID;
    private int _fishLevel;
    private int _curRoundCircle;
    private int _totalRound;
    private int _curRound;
    private int _successCount;
    private int _successNeedCount;
    private int _innerCircleWeight;
    private int _outerCircleWeight;

    public override void Init()
    {
        _innerCircleWeight = 1;
        _outerCircleWeight = 1;
    }

    public void StartFishingInteraction(int id,int level)
    {
        _fishID = id;
        _fishLevel = level;
        _curRound = 0;
        _totalRound = 0;
        _successNeedCount = 0;
        _successCount = 0;
        switch (_fishLevel)
        {
            case 1:
                _totalRound = 3;
                _successNeedCount = 2;
            break;
            case 2:
                _totalRound = 6;
                _successNeedCount = 4;
                break;
            case 3:
                _totalRound = 9;
                _successNeedCount = 6;
                break;
        }
        UIManager.Instance.OpenWindow(Const.FishingInteractionWindow);
        PlayEffect();
    }

    private void PlayEffect()
    {
        EventCenter.Instance.TriggerEvent("UpdateLeftRoundText", _totalRound - _curRound);
        _curRound++;
        if(_successCount == _successNeedCount)
        {
            FishingEnd();
            GetReward();
            return;
        }
        if(_curRound > _totalRound)
        {
            FishingEnd();
            return;
        }
        _curRoundCircle = Random.Range(1,5);
        EventCenter.Instance.TriggerEvent("SetYellowCircle",_curRoundCircle);
    }

    public void FishingSettlemment(bool issuccess)
    {
        bool isSuccess = issuccess;
        if(isSuccess)
        {
            FishingSuccess();
        }
        else
        {
            FishingFail();
        }
    }

    private void FishingFail()
    {
        PlayEffect();
    }

    private void FishingSuccess()
    {
        _successCount++;
        PlayEffect();
    }

    private void GetReward()
    {
        UIManager.Instance.OpenWindow(Const.PostWindow);
        UIManager.Instance.GetWindow(Const.PostWindow).GetComponent<PostWindow>().SetPost("��");
    }

    private void FishingEnd()
    {
        _curRound = 0;
        _totalRound = 0;
        _successNeedCount = 0;
        _successCount = 0;
        QuitFishing();
        UIManager.Instance.CloseWindow(Const.FishingInteractionWindow);
    }

    public void QuitFishing()
    {
        EventCenter.Instance.TriggerEvent("SetPlayerNeedCheck", true);
        EventCenter.Instance.TriggerEvent("SetBuoy");
        EventCenter.Instance.TriggerEvent("SetIsFishing", false);
        EventCenter.Instance.TriggerEvent("SetPlayerInputMapActivate", true);
        GameManager.Instance.ChangePlayerCameraFellow(GameObject.FindWithTag("Player").transform);
    }
}
