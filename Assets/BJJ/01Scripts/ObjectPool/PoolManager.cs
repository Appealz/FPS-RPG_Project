using System.Collections.Generic;
using UnityEngine;

public enum PoolType
{
    Effect,
    Enemy,

}

public class PoolManager : DestroySingleton<PoolManager>
{
    private Dictionary<string, Pool> poolDic = new Dictionary<string, Pool>();

    public void InitPoolManager()
    {
        EnemySet();
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
