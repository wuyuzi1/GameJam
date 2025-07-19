using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    private Transform _begin;
    private Transform _end;
    private Transform _buoy;
    private LineRenderer _rodLineRenderer;
    private LineRenderer _buoyLineRenderer;

    public List<FishingRodOffset> pointOffset;
    private float _strength;

    private void Awake()
    {
        _begin = transform.GetChild(0);
        _end = transform.GetChild(1);
        _rodLineRenderer = GetComponent<LineRenderer>();
        _buoyLineRenderer = _begin.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (_buoy != null)
        {
            DrowBuoyLine();
            DrawFishingRod();
        }
    }

    private void DrawFishingRod()
    {
        List<Vector3> path = GetPoints();
        _rodLineRenderer.positionCount = path.Count;
        _rodLineRenderer.SetPositions(path.ToArray());
    }

    private List<Vector3> GetPoints()
    {
        List<Vector3> points = new List<Vector3>();
        List<Vector3> tempPoints = new List<Vector3>();
        tempPoints.Add(_begin.position);
        for(int i=0;i<pointOffset.Count;i++)
        {
            Vector3 middlePoint = _begin.position - (_begin.position-_end.position) * pointOffset[i].offsety;
            middlePoint = middlePoint + (Vector3)Vector2.Perpendicular(_begin.position - _end.position) * pointOffset[i].offsetx * _strength;
            tempPoints.Add(middlePoint);
        }
        for (int i = 1; i < 100; i++)
        {
            points.Add(BezierHelper.Bezier(tempPoints, i / 100f));
        }
        points.Add(_end.position);
        return points;
    }

    private void DrowBuoyLine()
    {
        _buoyLineRenderer.positionCount = 2;
        _buoyLineRenderer.SetPosition(0, _begin.position);
        _buoyLineRenderer.SetPosition(1, _buoy.position);
        float distance = (_buoy.position.y - _end.position.y) * 0.2f;//(_buoy.position.x -_end.position.x) * 0.2f;
        _strength = Mathf.Clamp(distance, -2f, 2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (_buoy != null)
        {
            Gizmos.DrawWireSphere(_buoy.position, 1.5f);
        }
    }

    public void SetBuoy(Transform buoy = null)
    {
        _buoy = buoy;
    }
}

[System.Serializable]
public struct FishingRodOffset
{
    public float offsetx;
    public float offsety;
}
