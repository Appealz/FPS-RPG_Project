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
        // 1. ������ �ε�        
        if (!DataManager.Instance.GetWeaponData(weaponID, out var weaponData))
        {
            Debug.LogError($"[WeaponManager] ���� �����Ͱ� �������� �ʽ��ϴ�. ID: {weaponID}");
            return;
        }
 
        
        // 2. ������ �ε�
        // todo : ������ؼ� resources �������� �������ų� addressable �̿�

        // 3. ������Ʈ ����
        // todo : ������ �������� �̿��ؼ� Instantiate
        // GameObject obj = Instantiate        

        // 4. ������ ����
        // todo: ������ ������ ������ IWeapon Ŭ������ ���ؼ� ������ ����.
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
