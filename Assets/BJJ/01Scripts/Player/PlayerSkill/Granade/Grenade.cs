using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Grenade : MonoBehaviour, ISkillObject, IPoolLabel
{
    private Pool ownerPool;
    private GameObject ownerObj;
    private IRifleSkill rifleSkill;

    public void Create(Pool onwerPool)
    {
        this.ownerPool = onwerPool;
        gameObject.SetActive(false);
    }

    public void InitSpawnObj(GameObject ownerObj, ClassSkillData data)
    {
        this.ownerObj = ownerObj;
        
        if(data is IRifleSkill rifle)
        {
            rifleSkill = rifle;
        }

        ExplosionDelay().Forget();
    }

    private async UniTaskVoid ExplosionDelay()
    {
        await UniTask.WaitForSeconds(3f);

        Explosion();
    }

    private void Explosion()
    {
        var explosion = PoolManager.Instance.GetPool("Explosion01").GetObjFromPool();
        explosion.transform.position = transform.position;
        if (explosion.TryGetComponent<IEffectObject>(out var efx))
        {
            efx.EffectStart();
        }
        Collider[] objs = Physics.OverlapSphere(transform.position, 8f);
        HashSet<GameObject> hits = new HashSet<GameObject>();

        foreach (Collider obj in objs)
        {
            if(obj.TryGetComponent<IHitPart>(out var hit))
            {
                hits.Add(hit.owner.ReciverGO);
            }
        }

        foreach(var hit in hits)
        {
            EventBus_Damage.Publish(new DamageInfo(ownerObj, hit, rifleSkill.Damage, null, DamageType.Damage));
        }

        ReturnToPool();
    }

    public void ReturnToPool()
    {
        ownerPool.ReturnToPool(gameObject);
    }
}
