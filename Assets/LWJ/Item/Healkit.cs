using UnityEngine;

public class Healkit : MonoBehaviour, IHealkit
{
    public bool useable => throw new System.NotImplementedException();
    public AnimationClip useClip => throw new System.NotImplementedException();

    public AnimationClip reloadClip => throw new System.NotImplementedException();

    public AnimEventData useAnimData => throw new System.NotImplementedException();

    public int itemID => throw new System.NotImplementedException();

    public void InitData()
    {
        
    }

    public void InitData(Healkit_Entity newData)
    {
        
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
}
