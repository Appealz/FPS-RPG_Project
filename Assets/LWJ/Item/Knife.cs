using UnityEngine;

public class Knife : MonoBehaviour, IMeleeWeapon , IPoolLabel
{
    private Pool ownerPool;
    public bool useable => throw new System.NotImplementedException();

    public AnimEventData useAnimData => throw new System.NotImplementedException();

    public int itemID => throw new System.NotImplementedException();

    private CurrentData currentData;
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
        return currentData;
    }

    public void InitData(ItemData newData)
    {
        
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
