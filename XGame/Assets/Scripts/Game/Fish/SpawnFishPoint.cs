using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFishPoint : MonoBehaviour
{
    private float _radius;
    public float _radiusMax;
    public float _radiusMin;

    public Vector2 GetPointInRadius()
    {
        float x = Random.Range(-_radius, _radius);
        float y = Random.Range(-_radius, _radius);
        return new Vector2(x, y);
    }

    public void SpawnFish()
    {
        
    }

}
