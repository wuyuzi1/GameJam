using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _moveSpeed;
    private Rigidbody2D _playerRb;
    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _moveSpeed = 5f;
    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        if(PlayerInput.Instance.MoveInput.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(PlayerInput.Instance.MoveInput.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        _playerRb.velocity = PlayerInput.Instance.MoveInput * _moveSpeed;
    }
}
