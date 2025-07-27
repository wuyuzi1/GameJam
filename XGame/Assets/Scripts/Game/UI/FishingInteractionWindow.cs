using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishingInteractionWindow : MonoBehaviour, IWindow
{
    private Transform _blueCircle;
    private Transform _yellowCircle;
    private Transform _lastYellowCircle;
    private Button _fishingButton;
    private TextMeshProUGUI _roundLeft;
    private float _shrinkTotalTime;
    private int _selectRound;

    private void Awake()
    {
        _blueCircle = transform.Find("BlueCircle");
        _yellowCircle = transform.Find("YellowCircle");
        _fishingButton = transform.Find("FishingButton").GetComponent<Button>();
        _roundLeft = transform.Find("RoundLeft").GetComponent<TextMeshProUGUI>();
    }

    public void Close()
    {
        
    }

    public void Open()
    {
        for(int i=0;i<_yellowCircle.childCount;i++)
        {
            _yellowCircle.GetChild(i).gameObject.SetActive(false);
        }
        _blueCircle.gameObject.SetActive(true);
        _lastYellowCircle = null;
        _shrinkTotalTime = 3f;
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddListener("SetYellowCircle", SetYellowCircle);
        EventCenter.Instance.AddListener("UpdateLeftRoundText", UpdateLeftRoundText);
        _fishingButton.onClick.AddListener(InteracteClick);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("SetYellowCircle", SetYellowCircle);
        EventCenter.Instance.RemoveListener("UpdateLeftRoundText", UpdateLeftRoundText);
        _fishingButton.onClick.RemoveListener(InteracteClick);
    }

    private void Update()
    {

    }

    private void StartBuleCircleShrink()
    {
        _blueCircle.localScale = Vector3.one;
        StartCoroutine("CircleShrink");
    }

    private IEnumerator CircleShrink()
    {
        float t = 0;
        while(_blueCircle.localScale.x > 0.1f)
        {
            t+=Time.deltaTime/_shrinkTotalTime;
            float curScale = _blueCircle.localScale.x;
            curScale = Mathf.Lerp(1f, 0f, t);
            _blueCircle.localScale = new Vector3(curScale, curScale, curScale);
            yield return null;
        }
        _blueCircle.localScale = Vector3.zero;
        FishingSystem.Instance.FishingSettlemment(false);
    }

    private void SetYellowCircle(object[] args)
    {
        _selectRound = (int)args[0];
        if(_lastYellowCircle!=null)
        {
            _lastYellowCircle.gameObject.SetActive(false);
        }
        _yellowCircle.GetChild(_selectRound-1).gameObject.SetActive(true);
        _lastYellowCircle = _yellowCircle.GetChild(_selectRound - 1);
        StartBuleCircleShrink();
    }

    private void UpdateLeftRoundText(object[] args)
    {
        int leftround = (int) args[0];
        _roundLeft.text = $" £”‡¬÷¥Œ£∫{leftround}";
    }

    private void InteracteClick()
    {
        StopCoroutine("CircleShrink");
        float scoreScale = _blueCircle.localScale.x;
        Debug.Log("InteracteClick----"+scoreScale+"-------"+_selectRound);
        if (scoreScale< 0.2f)
        {
            TimerManager.Instance.GetOneTimer(0.25f, () =>
             {
                 FishingSystem.Instance.FishingSettlemment(false);
             });
        }
        else if(scoreScale < 0.4f)
        {
            if(_selectRound == 1)
            {
                TimerManager.Instance.GetOneTimer(0.25f, () =>
                {
                    FishingSystem.Instance.FishingSettlemment(true);
                });
            }
            else
            {
                TimerManager.Instance.GetOneTimer(0.25f, () =>
                {
                    FishingSystem.Instance.FishingSettlemment(false);
                });
            }
        }
        else if(scoreScale < 0.6f)
        {
            if (_selectRound == 2)
            {
                TimerManager.Instance.GetOneTimer(0.25f, () =>
                {
                    FishingSystem.Instance.FishingSettlemment(true);
                });
            }
            else
            {
                TimerManager.Instance.GetOneTimer(0.25f, () =>
                {
                    FishingSystem.Instance.FishingSettlemment(false);
                });
            }
        }
        else if(scoreScale < 0.8f)
        {
            if (_selectRound == 3)
            {
                TimerManager.Instance.GetOneTimer(0.25f, () =>
                {
                    FishingSystem.Instance.FishingSettlemment(true);
                });
            }
            else
            {
                TimerManager.Instance.GetOneTimer(0.25f, () =>
                {
                    FishingSystem.Instance.FishingSettlemment(false);
                });
            }
        }
        else if(scoreScale < 1f)
        {
            if (_selectRound == 4)
            {
                TimerManager.Instance.GetOneTimer(0.25f, () =>
                {
                    FishingSystem.Instance.FishingSettlemment(true);
                });
            }
            else
            {
                TimerManager.Instance.GetOneTimer(0.25f, () =>
                {
                    FishingSystem.Instance.FishingSettlemment(false);
                });
            }
        }
    }
}
