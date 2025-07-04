using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private IPoolLabel originPrefab;
    private Stack<IPoolLabel> stk = new Stack<IPoolLabel>();
    private int createCount;

    public void InitPool(IPoolLabel newLabel, int newCount = 10)
    {
        originPrefab = newLabel;
        createCount = newCount;
        Allocate();
    }

    private void Allocate()
    {
        for(int i = 0; i < createCount; i++)
        {
            GameObject obj = ObjectPoolFactory.Instance.CreateObj(originPrefab);
            if(obj.TryGetComponent<IPoolLabel>(out IPoolLabel label))
            {
                label.Create(this);
                stk.Push(label);
            }
        }
    }

    private IPoolLabel GetLabelFromPool()
    {
        if (stk.Count == 0)
            Allocate();
        return stk.Pop();
    }

    public GameObject GetObjFromPool()
    {
        var label = GetLabelFromPool();

        if(label is MonoBehaviour mono)
        {
            mono.gameObject.SetActive(true);
            return mono.gameObject;
        }

        Debug.Log($"{gameObject} Pool - GetObjFromPool() - Can't Find Obj");
        return null;
    }

    public void ReturnToPool(GameObject obj)
    {
        if(obj.TryGetComponent<IPoolLabel>(out IPoolLabel label))
        {
            obj.SetActive(false);
            stk.Push(label);
        }
    }
}
