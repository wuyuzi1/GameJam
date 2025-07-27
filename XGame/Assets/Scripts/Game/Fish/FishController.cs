using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class FishController : MonoBehaviour
{
    public FishSO fishSO;
    public int fishID;
    public int fishLevel;
    private CircleCollider2D _collider;
    private StateMachine _stateMachine;
    private LayerMask _fishLayer;
    private SpawnFishPoint _spawnFishPoint;
    

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        InitStateMachine();
    }

    private void Start()
    {
        _fishLayer = 1 << 8;
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddListener("CancelAllChase", CancelAllChase);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("CancelAllChase", CancelAllChase);
    }

    private void Update()
    {
        _stateMachine.UpdateState(Time.deltaTime);
    }

    public void Init(FishConfig fishConfig,int fishlevel)
    {
        fishSO = fishConfig.fishSO;
        fishID = fishConfig.fishID;
        fishLevel = fishlevel;
        _collider.radius = fishSO.radius;
        _stateMachine.ChangeState(EStateID.FishIdle);
    }

    private void InitStateMachine()
    {
        _stateMachine = new StateMachine(this.gameObject);
        _stateMachine.AddState(EStateID.FishIdle);
        _stateMachine.AddState(EStateID.FishPatrol);
        _stateMachine.AddState(EStateID.FishChase);
    }

    public void SetSpawnFishPoint(SpawnFishPoint spawnFishPoint)
    {
        _spawnFishPoint = spawnFishPoint;
    }

    public Vector3 GetMoveTarget()
    {
        return _spawnFishPoint.transform.TransformPoint(_spawnFishPoint.GetPointInRadius());
    }

    private void CancelAllChase(object[] args)
    {
        Debug.Log("CancelAllChase");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 35f, _fishLayer);
        for(int i=0;i<colliders.Length;i++)
        {
            colliders[i].GetComponent<FishController>().ChangeToPatrol();
        }
    }

    public void ChangeToPatrol()
    {
        _stateMachine.ChangeState(EStateID.FishPatrol);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            _stateMachine.ChangeState(EStateID.FishChase);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (fishSO != null)
        {
            Gizmos.DrawWireSphere(transform.position, _collider.radius);
        }
    }
}
