using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="FishSO",menuName ="ScriptableObject/FishSO")]
public class FishSO : ScriptableObject
{
    public int fishLevel;
    public Sprite fishIcon;
    public float speed;
    public float radius;
}
