using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishChaseState : StateBase
{
    private Transform _buoyTarget;
    private FishController _fishController;

    public FishChaseState(StateMachine machine) : base(machine)
    {
        _fishController = _machine.Holder.GetComponent<FishController>();
    }

    public override void Enter()
    {
        _buoyTarget = GameObject.FindGameObjectWithTag("Player").transform.Find("Buoy");
        _machine.Holder.transform.up = (_buoyTarget.position - _machine.Holder.transform.position).normalized;
    }

    public override void Exit()
    {
        
    }

    public override void Update(float dt)
    {
        _machine.Holder.transform.position = Vector3.MoveTowards(_machine.Holder.transform.position, _buoyTarget.position, _fishController.fishSO.speed * dt);
        if ((_machine.Holder.transform.position - _buoyTarget.position).sqrMagnitude < 0.01f)
        {
            EventCenter.Instance.TriggerEvent("CancelAllChase");
            FishingSystem.Instance.StartFishingInteraction(_fishController.fishID,_fishController.fishLevel);
            GameObject.Destroy(_machine.Holder.gameObject);
        }
    }
}
