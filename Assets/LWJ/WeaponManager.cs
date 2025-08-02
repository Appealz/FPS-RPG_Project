using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;
using UnityEngine.ResourceManagement.AsyncOperations;


// 캐싱된 아이템 데이터 리스트 오픈
public class WeaponManager : DestroySingleton<WeaponManager>
{
    private Dictionary<int, Pool> weaponPoolDic = new Dictionary<int, Pool>();

    Dictionary<int, GameObject> weapons = new Dictionary<int, GameObject>();
    Dictionary<int, GameObject> playerWeapon = new Dictionary<int, GameObject>();

    Dictionary<int, ItemData> itemDatas = new Dictionary<int, ItemData>();

    [SerializeField]
    List<GameObject> weaponList = new List<GameObject>();


    // 게임 시작시 WeaponManager에 플레이타임동안 사용할 데이터 로드
    public async UniTask CreateItemData()
    {
        // 주입받은 메인슬롯의 무기 레벨        
        //ClassData selectClassData = ContextManager.Instance.GetPlayGameContext().playClassData;

        // Test
        ClassData selectClassData = ContextManager.Instance.TestPlayGameContext().playClassData;
        int playerMainWeaponID = selectClassData.GetEquippedItemID(itemSlotType.Main);
        DataManager.Instance.GetWeaponData(playerMainWeaponID, out WeaponData_Entity saveWeaponData);
        //int weaponLevel = saveWeaponData.weaponLevel;
        //List<WeaponData_Entity> weaponIDList = DataManager.Instance.GetWeaponList();
        //foreach (var weapon in weaponIDList)
        //{
        //    if (weapon.weaponLevel >= weaponLevel)
        //    {
        //        itemDatas[weapon.id] = new WeaponData(weapon);
        //        await CreateWeaponPool(weapon.id);
        //    }
        //}

        itemDatas[saveWeaponData.id] = new WeaponData(saveWeaponData);
        await CreateWeaponPool(saveWeaponData.id);
    }

    private async UniTask CreateWeaponPool(int id)
    {
        var prefab = await PrefabLoad.LoadToPrefab(id, PrefabType.Weapon);
        if(prefab == null)
        {
            Debug.Log("프리팹 로드 실패");
            return;
        }
        if(!prefab.TryGetComponent<IPoolLabel>(out var label))
        {
            Debug.Log("IPoolLabel 참조 실패");
            return;
        }
        if(weaponPoolDic.ContainsKey(id))
        {
            Debug.Log("이미 생성된 풀 존재");
            return;
        }

        GameObject poolObj = new GameObject($"WeaponPool_{id}");
        poolObj.transform.SetParent(transform);
        
        Pool newPool = poolObj.AddComponent<Pool>();
        newPool.InitPool(label, 1);
        weaponPoolDic[id] = newPool;
    }

    public ItemData GetItemData(int weaponID)
    {
        if (!itemDatas.TryGetValue(weaponID, out var item) || item == null)
        {
            Debug.LogError($"[WeaponManager] weaponPoolDic에 ID {weaponID}에 대한 풀이 없습니다.");
            return null;
        }

        return item;

        //weaponPoolDic.TryGetValue(weaponID, out var item);
        //item.TryGetComponent<IItem>(out IItem newItemData);
        //return newItemData;
    }

    public GameObject EquipWeapon(int weaponID)
    {
        //weapons.TryGetValue(weaponID, out GameObject weapon);
        //return weapon;

        if (weaponPoolDic.TryGetValue(weaponID, out Pool pool))
        {
            GameObject weapon = pool.GetObjFromPool();

            if (weapon != null)
            {
                weapon.SetActive(true); // 필요 시 활성화
                return weapon;
            }
            else
            {
                Debug.LogWarning($"[WeaponManager] 무기 풀에서 무기 꺼내기 실패 - ID: {weaponID}");
                return null;
            }
        }
        else
        {
            Debug.LogError($"[WeaponManager] weaponPoolDic에 무기 ID {weaponID} 대한 풀 없음");
            return null;
        }
    }

    public IItem GetItemInterface(int weaponID)
    {
        if (weaponPoolDic.TryGetValue(weaponID, out Pool pool))
        {
            GameObject weapon = pool.GetObjFromPool();

            if (weapon != null)
            {
                weapon.TryGetComponent<IItem>(out IItem item);
                return item;
            }
            else
            {
                Debug.LogWarning($"[WeaponManager] 무기 풀에서 무기 꺼내기 실패 - ID: {weaponID}");
                return null;
            }
        }
        else
        {
            Debug.LogError($"[WeaponManager] weaponPoolDic에 무기 ID {weaponID} 대한 풀 없음");
            return null;
        }        
    }

    public void ReturnWeapon(GameObject returnWeapon)
    {
        returnWeapon.transform.SetParent(transform);
        returnWeapon.transform.localPosition = Vector3.zero;
        returnWeapon.SetActive(false);
    }

    public GameObject FindGetPool(int weaponID)
    {
        if(weaponPoolDic.TryGetValue(weaponID, out var pool))
        {
            return pool.GetObjFromPool();
        }

        Debug.Log("무기 풀 없음");
        return null;
    }

    public void FindReturnPool(GameObject obj)
    {
        if(obj.TryGetComponent<IItem>(out IItem item))
        {
            int id = item.itemID;
            if (weaponPoolDic.TryGetValue(id, out var pool))
            {
                pool.ReturnToPool(obj);
            }
            else
            {
                Debug.Log("무기 풀 없음");
            }

        }
        else
        {
            Debug.Log("IItem 참조 실패");
        }        
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
