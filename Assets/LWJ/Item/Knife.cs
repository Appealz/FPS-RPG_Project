using UnityEngine;

public class Knife : MonoBehaviour, IMeleeWeapon
{
    public bool useable => throw new System.NotImplementedException();

    public AnimationClip useClip => throw new System.NotImplementedException();    

    public AnimationClip reloadClip => throw new System.NotImplementedException();

    public void Attack()
    {
        
    }

    public void InitWeaponData(WeaponData newData)
    {
        
    }

    public void Use() => Attack();   

        


}
