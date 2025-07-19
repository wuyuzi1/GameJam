using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _moveSpeed;
    private Rigidbody2D _playerRb;
    private Transform _needFlip;
    private LayerMask _interactLayer;
    private List<Collider2D> _lastInteractList;
    private bool _needRayCheck;

    private void Awake()
    {
        _lastInteractList = new List<Collider2D>();
        _playerRb = GetComponent<Rigidbody2D>();
        _needFlip = transform.Find("NeedFlip");
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddListener("SetPlayerNeedCheck", SetPlayerNeedCheck);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("SetPlayerNeedCheck", SetPlayerNeedCheck);
    }

    private void Start()
    {
        _moveSpeed = 5f;
        _interactLayer = 1 << 9;
        _needRayCheck = true;
    }

    private void Update()
    {
        if(_needRayCheck)
        { 
            CheckInteract();
        }
        PlayerMove();
    }

    private void PlayerMove()
    {
        if(PlayerInput.Instance.MoveInput.x < 0 && _needFlip.localScale.x > 0)
        {
            _needFlip.localScale = new Vector3(-1, 1, 1);
        }
        else if(PlayerInput.Instance.MoveInput.x > 0 && _needFlip.localScale.x < 0)
        {
            _needFlip.localScale = new Vector3(1, 1, 1);
        }
        _playerRb.velocity = PlayerInput.Instance.MoveInput * _moveSpeed;
    }

    private void CheckInteract()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f,_interactLayer);
        if (colliders.Length<=0)
        {
            _lastInteractList.Clear();
            if (UIManager.Instance.GetWindow(Const.InteractionWindow) != null)
            { 
                UIManager.Instance.CloseWindow(Const.InteractionWindow);
            }
            return;
        }
        else
        {
            UIManager.Instance.OpenWindow(Const.InteractionWindow);
            UIManager.Instance.GetWindow(Const.InteractionWindow).transform.localPosition = new Vector3(0, 110f, 0);
            if (_lastInteractList.Count > colliders.Length)
            {
                _lastInteractList.Clear();
                UIManager.Instance.GetWindow(Const.InteractionWindow).GetComponent<InteractionWindow>().DestoryAll();
            }
            foreach (var item in colliders)
            {
                bool hasone = false;
                for (int i = 0; i < _lastInteractList.Count; i++)
                {
                    if (item == _lastInteractList[i])
                    {
                        hasone = true;
                        break;
                    }
                }
                if (hasone == false)
                {
                    GameObject buttonGo = GameObjectPool.Instance.GetFromPool("InteractionButton", UIManager.Instance.GetWindow(Const.InteractionWindow).transform);
                    IInterable interactable = item.GetComponent<IInterable>();
                    InteractionButton button = buttonGo.GetComponent<InteractionButton>();
                    button.SetText(interactable.ButtonName);
                    button.SetButtonAction(interactable.Interact);
                }
            }
            _lastInteractList = colliders.ToList<Collider2D>();
        }
    }

    private void SetPlayerNeedCheck(object[] args)
    {
        bool needCheck = (bool)args[0];
        _needRayCheck = needCheck;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
