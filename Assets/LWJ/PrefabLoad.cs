using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public enum PrefabType
{
    Weapon,
    HealKit,
    Enemy,
}

public static class PrefabLoad
{
    /// <summary>
    /// Addressables에서 프리팹을 비동기 로드하므로 반드시 await 키워드 사용.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newType"></param>
    /// <returns></returns>
    public static async UniTask<GameObject> LoadToPrefab(int id, PrefabType newType)
    {
        string path = null;
        // todo : 스위치문 타입별 프리팹 생성
        switch (newType)
        {
            case PrefabType.Weapon:
                if(DataManager.Instance.GetWeaponData(id, out WeaponData_Entity weaponData))
                {
                    path = weaponData.path;
                }
                break;
            case PrefabType.HealKit:
                if(DataManager.Instance.GetHealkitData(id, out Healkit_Entity healkitData))
                {
                    path = healkitData.path;
                }
                break;
            case PrefabType.Enemy:
                if(DataManager.Instance.GetMonsterData(id, out MonsterStats_Entity monsterData))
                {
                    path = monsterData.path;
                }
                break;
            default:
                Debug.Log("알수없는 프리팹 타입");
                return null;                
        }
        
        // todo : Entity 데이터 path 가져오기
        
        var prefab = Addressables.LoadAssetAsync<GameObject>(path);
        await prefab.Task;

        if (prefab.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"프리팹 로드 실패 - Address: {path}");
            return null;
        }

        return prefab.Result;
    }
}

public static class SpriteLoad
{
    public static async UniTask<Sprite> LoadToSprite(int id)
    {
        string path = null;
        DataManager.Instance.GetItemData(id, out ItemData item);
        path = item.iconPath;

        var sprite = Addressables.LoadAssetAsync<Sprite>(path);
        await sprite.Task;

        if (sprite.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"프리팹 로드 실패 - Address: {path}");
            return null;
        }

        return sprite.Result;
    }
}

