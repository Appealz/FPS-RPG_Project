using UnityEngine;

public class Knife : MonoBehaviour, IMeleeWeapon
{
    public bool useable => throw new System.NotImplementedException();

    public AnimationClip useClip => throw new System.NotImplementedException();

    public AnimationClip dropClip => throw new System.NotImplementedException();

    public AnimationClip reloadClip => throw new System.NotImplementedException();

    public void InitData()
    {
        
    }

    public void Use() => Slash();
    

    private void Slash()
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
