using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInterable 
{
    public string ButtonName
    {
        get; set;
    }

    public void Interact(Transform interactTrans);
}
