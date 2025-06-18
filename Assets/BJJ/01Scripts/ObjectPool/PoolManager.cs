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

    public void SetPool(PoolType type, string name)
    {
        // todo 리소스 폴더에서 해당 타입에 따른 폴더에 접근해서 찾고 싶은 오브젝트의 이름으로 탐색해서 풀을 만들어서 세팅함
        // 나중에 컨텍스트 매니저를 통해서 로드해야할 데이터가 정해지면
        // 게임매니저가 해당 데이터들을 기반으로 주르륵 SetPool을 호출
        GameObject poolObj = new GameObject($"Pool_{name}");
        Pool newPool = poolObj.AddComponent<Pool>();
        
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
