using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class WeaponManager : DestroySingleton<WeaponManager>
{    
    Dictionary<int, GameObject> weapons = new Dictionary<int, GameObject>();
    Dictionary<int, GameObject> playerWeapon = new Dictionary<int, GameObject>();

    [SerializeField]
    List<GameObject> weaponList = new List<GameObject>();

    public async void CreateWeapon(int weaponID)
    {
        // 1. 데이터 로드        
        if (!DataManager.Instance.GetWeaponData(weaponID, out var weaponData))
        {
            Debug.LogError($"[WeaponManager] 무기 데이터가 존재하지 않습니다. ID: {weaponID}");
            return;
        }
        
        // 2. 프리펩 로드
        GameObject weaponPrefab = await LoadWeaponPrefab(weaponID);                                

        // 3. 오브젝트 생성        
        GameObject obj = Instantiate(weaponPrefab,transform);

        // 4. 데이터 주입
        // todo: 가져온 프리팹 내부의 IWeapon 클래스를 통해서 데이터 주입.
        if(obj.TryGetComponent<IWeapon>(out IWeapon newWeapon))
        {
            newWeapon.InitWeaponData(weaponData);
            weapons.Add(weaponID, obj);
            obj.SetActive(false);
        }        
    }

    private async UniTask<GameObject> LoadWeaponPrefab(int weaponID)
    {
        string address;
        switch (weaponID)
        {
            case int id when id >= 1001 && id <= 1005:
                address = "Assets/LWJ/Prefab/Rifle.prefab";
                break;
            case int id when id >= 1006 && id <= 1010:
                address = "Assets/LWJ/Prefab/ShotGun.prefab";
                break;
            case int id when id >= 1011 && id <= 1015:
                address = "Assets/LWJ/Prefab/HeavyGun.prefab";
                break;
            case 1016:
                address = "Assets/LWJ/Prefab/Revolver.prefab";
                break;
            case 1017:
                address = "Assets/LWJ/Prefab/Knife.prefab";
                break;
            default:
                Debug.LogError($"[WeaponManager] 정의되지 않은 무기 ID: {weaponID}");
                return null;
        }

        var weaponPrefab = Addressables.LoadAssetAsync<GameObject>(address);
        await weaponPrefab.Task;

        if (weaponPrefab.Status == AsyncOperationStatus.Succeeded)
        {
            return weaponPrefab.Result; 
        }
        else
        {
            Debug.LogError("프리팹 로드 실패!");
            return null;
        }
    }

    //public void PlayerEquipWeapon()
    //{
    //    ClassData classData = ContextManager.Instance.GetPlayGameContext().playClassData;
    //    List<int> equipWeaponID = classData.equippedItemIds;

    //    foreach (int weaponID in equipWeaponID)
    //    {
    //        CreateWeapon(weaponID);
            
    //    }
    //}

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

//public class WeaponData
//{
//    public int id;
//    public string name;
//    public float damagePerShot;
//    public float fireRate;
//    public int ammoPerReload;
//    public int maxAmmo;
//    public float range;
//    public float weight;
//    public int price;
//    public int weaponLevel;

//    public WeaponData(WeaponData_Entity newData)
//    {
//        id = newData.id;
//        name = newData.name;
//        damagePerShot = newData.damagePerShot;
//        fireRate = newData.fireRate;
//        ammoPerReload = newData.ammoPerReload;
//        maxAmmo = newData.maxAmmo;
//        range = newData.range;
//        weight = newData.weight;
//        price = newData.price;
//        weaponLevel = newData.weaponLevel;            
//    }
//}

//public interface IWeaponData
//{

//}

//public class MeleeWeaponData : IWeaponData
//{
//    public int id;
//    public string name;
//    public float damage;
//    public float attackRate;
//    public float range;

//    public MeleeWeaponData(WeaponData_Entity newData)
//    {
//        id = newData.id;
//        name = newData.name;
//        damage = newData.damagePerShot;
//        attackRate = newData.fireRate;
//        range = newData.range;
//    }
//}
//public class RangeWeaponData : IWeaponData
//{
//    public int id;
//    public string name;
//    public float damagePerShot;
//    public float fireRate;
//    public int ammoPerReload;
//    public int maxAmmo;
//    public float range;
//    public float weight;
//    public int price;
//    public int weaponLevel;

//    public RangeWeaponData(WeaponData_Entity newData)
//    {
//        id = newData.id;
//        name = newData.name;
//        damagePerShot = newData.damagePerShot;
//        fireRate = newData.fireRate;
//        ammoPerReload = newData.ammoPerReload;
//        maxAmmo = newData.maxAmmo;
//        range = newData.range;
//        weight = newData.weight;
//        price = newData.price;
//        weaponLevel = newData.weaponLevel;
//    }
//}
