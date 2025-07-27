using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FishingSite : MonoBehaviour, IInterable
{
    private string _buttonName = "Еігу";
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

    private BoxCollider2D _box;

    public SerializeDic<int,List<FishConfig>> fishGroup;
    public int spwanFishCount;
    public int spwanFishRadius;

    public float levelOneProbability;
    public float levelTwoProbability;

    private void Awake()
    {
        _box = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        fishGroup.SetSerializeDic();
        SpwanFishPointGenerate();
    }

    public void Interact(Transform interactTrans)
    {
        Transform buoy = interactTrans.Find("Buoy");
        buoy.transform.localPosition = GetBuoyInitPosition(interactTrans);
        Vector2 clampX = new Vector2(transform.position.x - _box.bounds.extents.x, transform.position.x + _box.bounds.extents.x);
        Vector2 clamoY = new Vector2(transform.position.y - _box.bounds.extents.y, transform.position.y + _box.bounds.extents.y);
        EventCenter.Instance.TriggerEvent("SetIsFishing", true , clampX,clamoY);
        EventCenter.Instance.TriggerEvent("SetBuoy",buoy.transform);
        EventCenter.Instance.TriggerEvent("ShowFishingStartButton", true);
    }

    private Vector3 GetBuoyInitPosition(Transform interactTrans)
    {
        if (interactTrans.position.x > transform.position.x + _box.bounds.extents.x)
        {
            return new Vector3(-5,0,0);
        }
        else if(interactTrans.position.x < transform.position.x + _box.bounds.extents.x)
        {
            return  new Vector3(5, 0, 0);
        }
        else if(interactTrans.position.y > transform.position.y + _box.bounds.extents.y)
        {
            return new Vector3(0, -5, 0);
        }
        else if(interactTrans.position.y < transform.position.y + _box.bounds.extents.y)
        {
            return new Vector3(0, 5, 0);
        }
        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(30, 30));
    }

    private void SpwanFishPointGenerate()
    {
        float xmin = transform.position.x - _box.bounds.extents.x + spwanFishRadius;
        float xmax = transform.position.x + _box.bounds.extents.x - spwanFishRadius;
        float ymin = transform.position.y - _box.bounds.extents.y + spwanFishRadius;
        float ymax = transform.position.y + _box.bounds.extents.y - spwanFishRadius;
        for(int i=0;i<spwanFishCount;i++)
        {
            GameObject go = Resources.Load<GameObject>("Game/Prefab/Normal/SpawnFishPoint");
            GameObject spwanPoint = Instantiate(go);
            spwanPoint.transform.position = new Vector2(Random.Range(xmin, xmax), Random.Range(ymin, ymax));
            spwanPoint.transform.parent = this.transform;
        }
    }
}

[System.Serializable]
public struct FishConfig
{
    public int fishID;
    public FishSO fishSO;
}
