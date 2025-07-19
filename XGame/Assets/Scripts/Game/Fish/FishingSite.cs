using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FishingSite : MonoBehaviour, IInterable
{
    private string _buttonName = "Fishing";
    public string ButtonName {
        get 
        {
            return _buttonName;
        }
        set
        {
            _buttonName = value;
        }
    }

    public void Interact()
    {
        Debug.Log("FishingSiteInteract");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(13, 13));
    }
}
