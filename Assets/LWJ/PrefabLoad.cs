using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

public enum PrefabType
{
    Weapon,
    HealKit,
    Enemy,
}

public static class PrefabLoad
{
    //public async static GameObject LoadToPrefab(int id, PrefabType newType)
    //{
    //    // todo : ����ġ�� Ÿ�Ժ� ������ ����
    //    DataManager.Instance.GetWeaponData(id, out WeaponData_Entity data);
    //    // todo : Entity ������ path ��������
    //    //string path = data.path;
    //    var Prefab = Addressables.LoadAssetAsync<GameObject>(path);
    //    await Prefab.Task;

    //    return Prefab.Result;    
    //}
}

