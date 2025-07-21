using UnityEngine;

public class Knife : MonoBehaviour, IMeleeWeapon , IPoolLabel
{
    private Pool ownerPool;
    public bool useable => throw new System.NotImplementedException();

    public AnimationClip useClip => throw new System.NotImplementedException();    

    public AnimationClip reloadClip => throw new System.NotImplementedException();

    public AnimEventData useAnimData => throw new System.NotImplementedException();

    public int itemID => throw new System.NotImplementedException();

    public void Attack()
    {
        
    }

    public void Create(Pool onwerPool)
    {
        this.ownerPool = onwerPool;
        gameObject.SetActive(false);
    }

    public CurrentData GetItemCurrentData()
    {
        throw new System.NotImplementedException();
    }

    public void InitData(ItemData newData)
    {
        throw new System.NotImplementedException();
    }

    public void InitWeaponData(WeaponData_Entity newData)
    {
        
    }

    public void ReturnToPool()
    {
        ownerPool.ReturnToPool(gameObject);
    }

    public void Use() => Attack();   

        


}
