using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;

public enum PoolType
{
    Effect,
    Enemy,

}

public class PoolManager : DestroySingleton<PoolManager>
{
    private Dictionary<string, Pool> poolDic = new Dictionary<string, Pool>();

    public async void PoolRegist(string path)
    {
        var obj = await Addressables.LoadAssetAsync<GameObject>(path).ToUniTask();

        if(obj == null)
        {
            Debug.Log($"PoolManager.cs - PoolRegist()- {path} is ErrorKey");
        }
        else
        {
            if(!obj.TryGetComponent<IPoolLabel>(out var label))
            {
                Debug.Log($"PoolManager.cs - PoolRegist()- {obj.name} Can't Find PoolLabel");
            }
            else
            {
                GameObject poolObj = new GameObject();
                poolObj.transform.parent = transform;
                poolObj.name = obj.name;
                Pool newPool = poolObj.AddComponent<Pool>();
                newPool.InitPool(label);
                poolDic[obj.name] = newPool;
            }
        }
    }

    private void EnemySet()
    {
        GameObject[] objs = Resources.LoadAll<GameObject>("Enemy");
        foreach (GameObject obj in objs)
        {
            if(obj.TryGetComponent<IPoolLabel>(out var label))
            {
                GameObject poolObj = new GameObject();
                poolObj.transform.parent = transform;
                poolObj.name = obj.name;
                Pool newPool = poolObj.AddComponent<Pool>();
                newPool.InitPool(label);
                poolDic[obj.name] = newPool;
            }
        }
    }

    public Pool GetPool(string name)
    {
        if(poolDic.TryGetValue(name, out Pool pool))
        {
            return pool;
        }
        Debug.Log($"PoolManager.cs - GetPool() - {name} Pool Can't Find");
        return null;
    }
}
