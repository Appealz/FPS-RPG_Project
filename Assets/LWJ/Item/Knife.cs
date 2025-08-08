using UnityEngine;

public class Knife : MonoBehaviour, IMeleeWeapon
{
    
    public bool useable => throw new System.NotImplementedException();

    public AnimEventData useAnimData => null;

    public int itemID => myData.id;

    private CurrentData currentData;

    private WeaponData_Entity myData;
    public void Attack()
    {
        
    }


    float damage;
    float attackRate;
    public void InitData(ItemData newData)
    {
        if (newData is WeaponData weaponData)
        {            
            damage = weaponData.damagePerShot;
            attackRate = weaponData.fireRate;
            myData = weaponData.data;

            Debug.Log($"데이터 주입 성공 : {weaponData.name}, {weaponData.itemID}, {weaponData.fireRate}, {weaponData.ammoPerReload}, {weaponData.range}");

            currentData = new CurrentData
            {
                name = weaponData.name,
                damage = weaponData.damagePerShot,
                firerRate = weaponData.fireRate,
                ammoPerReload = weaponData.ammoPerReload,
                maxAmmo = weaponData.maxAmmo,
                currentMagazine = weaponData.maxAmmo,
                price = weaponData.price,
                level = weaponData.weaponLevel,
            };
        }
    }


    public CurrentData GetItemCurrentData()
    {        
        return currentData;
    }

    public void Use() => Attack();   

        


}
