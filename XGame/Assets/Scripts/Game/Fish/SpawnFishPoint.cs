using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFishPoint : MonoBehaviour
{
    [SerializeField] private float _radius;
    public float radiusMax;
    public float radiusMin;
    public int spawnFishCount;
    public FishingSite fishingSite;

    private void Start()
    {
        _radius = Random.Range(radiusMin, radiusMax);
        fishingSite = transform.parent.GetComponent<FishingSite>();
        SpawnFish();
    }

    public Vector3 GetPointInRadius()
    {
        float x = Random.Range(-_radius+1f, _radius-1f);
        float y = Random.Range(-_radius+1f, _radius-1f);
        return new Vector3(x, y, 0);
    }

    public void SpawnFish()
    {
        for(int i=0;i<spawnFishCount;i++)
        {
            GameObject fishShadow = GameObjectPool.Instance.GetFromPool("FishShadow",transform);
            fishShadow.transform.position = transform.TransformPoint(GetPointInRadius());
            float random = Random.Range(0f, 1f);
            List<FishConfig> fishGroup;
            int fishLevel = 0;
            if(random < fishingSite.levelOneProbability)
            {
                fishGroup = fishingSite.fishGroup.storeDic[1];
                fishLevel = 1;
            }
            else if(random < fishingSite.levelTwoProbability)
            {
                fishGroup = fishingSite.fishGroup.storeDic[2];
                fishLevel = 2;
            }
            else
            {
                fishGroup = fishingSite.fishGroup.storeDic[3];
                fishLevel = 3;
            }
            int getFish = Random.Range(0, fishGroup.Count);
            fishShadow.GetComponent<FishController>().Init(fishGroup[getFish],fishLevel);
            fishShadow.GetComponent<FishController>().SetSpawnFishPoint(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,_radius);
    }
}
