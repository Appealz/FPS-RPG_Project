using Cysharp.Threading.Tasks;
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
    /// Addressables���� �������� �񵿱� �ε��ϹǷ� �ݵ�� await Ű���� ���.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newType"></param>
    /// <returns></returns>
    public static async UniTask<GameObject> LoadToPrefab(int id, PrefabType newType)
    {
        string path = null;
        // todo : ����ġ�� Ÿ�Ժ� ������ ����
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
                Debug.Log("�˼����� ������ Ÿ��");
                return null;                
        }
        
        // todo : Entity ������ path ��������
        
        var prefab = Addressables.LoadAssetAsync<GameObject>(path);
        await prefab.Task;

        if (prefab.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"������ �ε� ���� - Address: {path}");
            return null;
        }

        return prefab.Result;
    }
}

