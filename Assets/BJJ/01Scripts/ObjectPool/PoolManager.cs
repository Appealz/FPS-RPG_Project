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
        // todo ���ҽ� �������� �ش� Ÿ�Կ� ���� ������ �����ؼ� ã�� ���� ������Ʈ�� �̸����� Ž���ؼ� Ǯ�� ���� ������
        // ���߿� ���ؽ�Ʈ �Ŵ����� ���ؼ� �ε��ؾ��� �����Ͱ� ��������
        // ���ӸŴ����� �ش� �����͵��� ������� �ָ��� SetPool�� ȣ��
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
