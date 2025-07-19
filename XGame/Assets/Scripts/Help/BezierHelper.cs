using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierHelper
{
    public static Vector3 Bezier(Vector3 p0,Vector3 p1,float t)
    {
        return (1-t)*p0 + t*p1;
    }

    public static Vector3 Bezier(Vector3 p0,Vector3 p1,Vector3 p2,float t)
    {
        Vector3 p0p1 = (1 - t) * p0 + t * p1;
        Vector3 p1p2 = (1 - t) * p1 + t * p2;
        Vector3 temp = (1-t) * p0p1 + t * p1p2;
        return temp;
    }

    public static Vector3 Bezier(List<Vector3> points,float t)
    {
        if(points.Count<2)
        {
            return points[0];
        }
        List<Vector3> nextPoins = new List<Vector3>();
        for(int i=0;i<points.Count-1;i++)
        {
            Vector3 temp = (1 - t) * points[i] + t * points[i+1];
            nextPoins.Add(temp);
        }
        return Bezier(nextPoins, t);
    }
}
