using UnityEngine;

public class ObjectPoolFactory : DestroySingleton<ObjectPoolFactory>
{
    public GameObject CreateObj(IPoolLabel createOrigin)
    {
        GameObject obj = Instantiate((createOrigin as MonoBehaviour).gameObject);
        return obj;
    }
}
