using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingInteractionWindow : MonoBehaviour, IWindow
{
    private Transform _blueCircle;
    private Transform _yellowCircle;
    private Transform _lastYellowCircle;
    private Button _fishingButton;
    private bool _isShrink;

    private void Awake()
    {
        _blueCircle = transform.Find("BlueCircle");
        _yellowCircle = transform.Find("YellowCircle");
        _fishingButton = transform.Find("StartFishingButton").GetComponent<Button>();
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
        _isShrink = true;
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddListener("SetYellowCircle", SetYellowCircle);
        EventCenter.Instance.AddListener("SetBlueCircleScale", SetBlueCircleScale);
        _fishingButton.onClick.AddListener(InteracteClick);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("SetYellowCircle", SetYellowCircle);
        EventCenter.Instance.RemoveListener("SetBlueCircleScale", SetBlueCircleScale);
        _fishingButton.onClick.RemoveListener(InteracteClick);
    }

    private void Update()
    {

    }

    private void SetBlueCircleScale(object[] args)
    {
        float scale = (float)args[0];
        _blueCircle.localScale = new Vector3(scale, scale, scale);
    }

    private void SetYellowCircle(object[] args)
    {
        int round = (int)args[0];
        if(_lastYellowCircle!=null)
        {
            _lastYellowCircle.gameObject.SetActive(false);
        }
        _yellowCircle.GetChild(round-1).gameObject.SetActive(true);
        _lastYellowCircle = _yellowCircle;
    }

    private void InteracteClick()
    {

    }
}
