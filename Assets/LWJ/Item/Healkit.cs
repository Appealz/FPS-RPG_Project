using UnityEngine;

public class Healkit : MonoBehaviour, IHealkit
{
    public bool useable => throw new System.NotImplementedException();
    public AnimationClip useClip => throw new System.NotImplementedException();

    public AnimationClip reloadClip => throw new System.NotImplementedException();

    public AnimEventData useAnimData => throw new System.NotImplementedException();

    public int itemID => throw new System.NotImplementedException();

    public float healAmount;

    public void InitData(ItemData newData)
    {
        if(newData is HealkitData healkitData)
        {
            healAmount = healkitData.healAmount;
        }
    }

    public void Use()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CurrentData GetItemCurrentData()
    {
        throw new System.NotImplementedException();
    }

    public void Create(Pool onwerPool)
    {
        throw new System.NotImplementedException();
    }

    public void ReturnToPool()
    {
        throw new System.NotImplementedException();
    }
}
