using Cysharp.Threading.Tasks;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour, IPoolLabel, IEffectObject
{
    private Pool ownerPool;

    public void Create(Pool onwerPool)
    {
        ownerPool = onwerPool;
        gameObject.SetActive(false);
    }

    public void EffectStart()
    {
        EffectDelay();
    }

    private async void EffectDelay()
    {
        await UniTask.WaitForSeconds(0.2f);
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        gameObject.transform.parent = ownerPool.transform;
        ownerPool.ReturnToPool(gameObject);
    }
}
