using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class FishController : MonoBehaviour
{
    public FishSO fishSO;
    private CircleCollider2D _collider;
    private StateMachine _stateMachine;
    private LayerMask _playerLayer;
    

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _stateMachine = new StateMachine(this.gameObject);
    }

    private void Start()
    {
        _collider.radius = fishSO.radius;
        _playerLayer = 1 << 7;
        _stateMachine.AddState(EStateID.FishIdle);
        _stateMachine.AddState(EStateID.FishPatrol);
        _stateMachine.AddState(EStateID.FishChase);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == _playerLayer)
        {
            _stateMachine.ChangeState(EStateID.FishChase);
        }
    }
}
