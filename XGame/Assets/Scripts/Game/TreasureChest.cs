using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TreasureChest : MonoBehaviour,IInterable
{
    private BoxCollider2D _collider;
    private string _buttonName = "¿ª±¦Ïä";
    public string ButtonName { 
        get
        {
            return _buttonName;
        }
        set
        {
            _buttonName = value;
        }
    }

    public void Interact(Transform interactTrans)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        _collider.enabled = false;
        EventCenter.Instance.TriggerEvent("SetPlayerNeedCheck", true);
        AudioManager.Instance.PlayEffectAudio("sound_openbox");
        TimerManager.Instance.GetOneTimer(0.6f, () =>
         {
             UIManager.Instance.OpenWindow(Const.PostWindow);
             UIManager.Instance.GetWindow(Const.PostWindow).GetComponent<PostWindow>().SetPost("½ð±Ò");
             Destroy(this.gameObject);
         });
    }

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }
}
