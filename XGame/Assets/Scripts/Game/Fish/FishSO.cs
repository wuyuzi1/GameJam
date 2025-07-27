using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="FishSO",menuName ="ScriptableObject/FishSO")]
public class FishSO : ScriptableObject
{
    public float speed;
    public float chaseSpeed;
    public float radius;
}
