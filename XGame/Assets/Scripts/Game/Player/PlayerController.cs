using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float buoyMoveSpeed;
    private Rigidbody2D _playerRb;
    private Transform _needFlip;
    private Transform _buoy;
    private Vector2 _fishingClampX;
    private Vector2 _fishingClampY;
    private LayerMask _interactLayer;
    private List<Collider2D> _lastInteractList;
    private bool _needRayCheck;
    private bool _isFishing;

    private void Awake()
    {
        _lastInteractList = new List<Collider2D>();
        _playerRb = GetComponent<Rigidbody2D>();
        _needFlip = transform.Find("NeedFlip");
        _buoy = transform.Find("Buoy");
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddListener("SetPlayerNeedCheck", SetPlayerNeedCheck);
        EventCenter.Instance.AddListener("SetIsFishing", SetIsFishing);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("SetPlayerNeedCheck", SetPlayerNeedCheck);
        EventCenter.Instance.RemoveListener("SetIsFishing", SetIsFishing);
    }

    private void Start()
    {
        _interactLayer = 1 << 9;
        _needRayCheck = true;
        _isFishing = false;
        _buoy.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(_needRayCheck)
        { 
            CheckInteract();
        }
        if (_isFishing)
        {
            FishingBuoyMove();
        }
        else
        {
            PlayerMove();
        }
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
        _playerRb.velocity = PlayerInput.Instance.MoveInput * moveSpeed;
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
                    button.SetButtonAction(interactable.Interact,this.transform);
                }
            }
            _lastInteractList = colliders.ToList<Collider2D>();
        }
    }

    private void SetPlayerNeedCheck(object[] args)
    {
        _needRayCheck = (bool)args[0];
    }

    private void SetIsFishing(object[] args)
    {
        _isFishing = (bool)args[0];
        _fishingClampX = (Vector2)args[1];
        _fishingClampY = (Vector2)args[2];
        _buoy.gameObject.SetActive(true);
    }

    private void FishingBuoyMove()
    {
        if(PlayerInput.Instance.MoveInput != Vector2.zero)
        {
            Vector3 target = PlayerInput.Instance.MoveInput * buoyMoveSpeed * Time.deltaTime;
            _buoy.Translate(target);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
