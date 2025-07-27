using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoSingleton<PlayerInput>
{
    private PlayerMap _playerInputMap;
    private Vector2 _moveInput;//ÕÊº“ ‰»Î
    public Vector2 MoveInput
    {
        get { return _moveInput; }
    }

    private new void Awake()
    {
        base.Awake();
       _playerInputMap = new PlayerMap();
    }

    private void OnEnable()
    {
        _playerInputMap.Enable();
        EventCenter.Instance.AddListener("SetPlayerInputMapActivate", SetPlayerInputMapActivate);
    }

    private void OnDisable()
    {
        _playerInputMap.Disable();
        EventCenter.Instance.RemoveListener("SetPlayerInputMapActivate", SetPlayerInputMapActivate);
    }

    private void OnDestroy()
    {
        _playerInputMap = null;
    }

    private void Update()
    {
        _moveInput = _playerInputMap.Player.Move.ReadValue<Vector2>();
    }

    private void SetPlayerInputMapActivate(object[] args)
    {
        bool enable = (bool)args[0];
        if (enable)
        {
            _playerInputMap.Enable();
        }
        else
        {
            _playerInputMap.Disable();
        }
    }

}
