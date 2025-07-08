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
    //    // todo : 스위치문 타입별 프리팹 생성
    //    DataManager.Instance.GetWeaponData(id, out WeaponData_Entity data);
    //    // todo : Entity 데이터 path 가져오기
    //    //string path = data.path;
    //    var Prefab = Addressables.LoadAssetAsync<GameObject>(path);
    //    await Prefab.Task;

    //    return Prefab.Result;    
    //}
}

