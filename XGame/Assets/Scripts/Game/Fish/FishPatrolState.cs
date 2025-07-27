using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPatrolState : StateBase
{
    private FishController _fishController;
    private Vector3 _target;
    public FishPatrolState(StateMachine machine) :base(machine)
    {
        _fishController = _machine.Holder.GetComponent<FishController>();
    }
    public override void Enter()
    {
        _target = _fishController.GetMoveTarget();
        Debug.Log("Patorl");
        _machine.Holder.transform.up = (_target - _machine.Holder.transform.position).normalized;
    }

    public override void Exit()
    {
        
    }

    public override void Update(float dt)
    {
        _machine.Holder.transform.position = Vector3.MoveTowards(_machine.Holder.transform.position, _target,_fishController.fishSO.speed * dt);
        if ((_machine.Holder.transform.position - _target).sqrMagnitude < 0.01f)
        {
            _machine.ChangeState(EStateID.FishIdle);
        }
    }
}
