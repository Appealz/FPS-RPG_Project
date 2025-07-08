using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;
using UnityEngine.ResourceManagement.AsyncOperations;


// ĳ�̵� ������ ������ ����Ʈ ����
public class WeaponManager : DestroySingleton<WeaponManager>
{
    private Dictionary<int, Pool> itemPoolDic = new Dictionary<int, Pool>();

    Dictionary<int, GameObject> weapons = new Dictionary<int, GameObject>();
    Dictionary<int, GameObject> playerWeapon = new Dictionary<int, GameObject>();

    Dictionary<int, ItemData> itemDatas = new Dictionary<int, ItemData>();

    [SerializeField]
    List<GameObject> weaponList = new List<GameObject>();


    private void CreateItemData()
    {
        // ���Թ��� ���ν����� ���� ����        
        ClassData selectClassData = ContextManager.Instance.GetPlayGameContext().playClassData;
        int playerMainWeaponID = selectClassData.GetEquippedItemID(itemSlotType.Main);
        DataManager.Instance.GetWeaponData(playerMainWeaponID, out WeaponData_Entity saveWeaponData);
        int weaponLevel = saveWeaponData.weaponLevel;

        List<WeaponData_Entity> weaponIDList = DataManager.Instance.GetWeaponList();
        foreach (var weapon in weaponIDList)
        {
            if (weapon.weaponLevel >= weaponLevel)
            {
                itemDatas[weapon.id] = new WeaponData(weapon);
            }
        }
    }

    public void CreatePool()
    {
        //GameObject[] objs = 
        //foreach (GameObject obj in objs)
        //{
        //    if (obj.TryGetComponent<IPoolLabel>(out var label))
        //    {
        //        GameObject poolObj = new GameObject();
        //        poolObj.transform.parent = transform;
        //        poolObj.name = obj.name;
        //        Pool newPool = poolObj.AddComponent<Pool>();
        //        newPool.InitPool(label);
        //        poolDic[obj.name] = newPool;
        //    }
        //}
    }

    public async void CreateWeapon(int weaponID)
    {
        // 1. ������ �ε�        
        if (!DataManager.Instance.GetWeaponData(weaponID, out var weaponData))
        {
            Debug.LogError($"[WeaponManager] ���� �����Ͱ� �������� �ʽ��ϴ�. ID: {weaponID}");
            return;
        }

        // 2. ������ �ε�
        GameObject weaponPrefab = await LoadWeaponPrefab(weaponID);

        // 3. ������Ʈ ����        
        GameObject obj = Instantiate(weaponPrefab, transform);

        // 4. ������ ����
        // todo: ������ ������ ������ IWeapon Ŭ������ ���ؼ� ������ ����.
        if (obj.TryGetComponent<IItem>(out IItem newItem))
        {
            WeaponData newWeaponData = new WeaponData(weaponData);
            newItem.InitData(newWeaponData);
            weapons.Add(weaponID, obj);
            itemDatas.Add(weaponID, newWeaponData);
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
                Debug.LogError($"[WeaponManager] ���ǵ��� ���� ���� ID: {weaponID}");
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
            Debug.LogError("������ �ε� ����!");
            return null;
        }
    }


    public IItem GetItemData(int weaponID)
    {
        itemPoolDic.TryGetValue(weaponID, out var item);
        item.TryGetComponent<IItem>(out IItem newItemData);
        return newItemData;
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
