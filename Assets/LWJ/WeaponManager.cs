using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : DestroySingleton<WeaponManager>
{    
    Dictionary<int, GameObject> weapons = new Dictionary<int, GameObject>();
    Dictionary<int, GameObject> playerWeapon = new Dictionary<int, GameObject>();

    [SerializeField]
    List<GameObject> weaponList = new List<GameObject>();

    public void CreateWeapon(int weaponID)
    {
        // 1. 데이터 로드        
        if (!DataManager.Instance.GetWeaponData(weaponID, out var weaponData))
        {
            Debug.LogError($"[WeaponManager] 무기 데이터가 존재하지 않습니다. ID: {weaponID}");
            return;
        }
 
        
        // 2. 프리펩 로드
        // todo : 경로통해서 resources 폴더에서 가져오거나 addressable 이용

        // 3. 오브젝트 생성
        // todo : 가져온 프리팹을 이용해서 Instantiate
        // GameObject obj = Instantiate        

        // 4. 데이터 주입
        // todo: 가져온 프리팹 내부의 IWeapon 클래스를 통해서 데이터 주입.
    }

    public void PlayerEquipWeapon()
    {
        ClassData classData = ContextManager.Instance.GetPlayGameContext().playClassData;
        List<int> equipWeaponID = classData.equippedItemIds;

        foreach (int weaponID in equipWeaponID)
        {
            CreateWeapon(weaponID);
            
        }
    }

    public GameObject EquipWeapon(int weaponID)
    {
        weapons.TryGetValue(weaponID, out GameObject weapon);
        return weapon;
    }

    public void ReturnWeapon(GameObject returnWeapon)
    {
        returnWeapon.transform.SetParent(transform);
        returnWeapon.transform.localPosition = Vector3.zero;
        returnWeapon.SetActive(false);
    }
}



public class WeaponData
{
    public int id;
    public string name;
    public float damagePerShot;
    public float fireRate;
    public int ammoPerReload;
    public int maxAmmo;
    public float range;
    public float weight;
    public int price;
    public int weaponLevel;

    public WeaponData(WeaponData_Entity newData)
    {
        id = newData.id;
        name = newData.name;
        damagePerShot = newData.damagePerShot;
        fireRate = newData.fireRate;
        ammoPerReload = newData.ammoPerReload;
        maxAmmo = newData.maxAmmo;
        range = newData.range;
        weight = newData.weight;
        price = newData.price;
        weaponLevel = newData.weaponLevel;            
    }
}
