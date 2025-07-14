using Cysharp.Threading.Tasks;
using UnityEngine;

public class Grenade : MonoBehaviour, ISkillObject, IPoolLabel
{
    private Pool ownerPool;
    private GameObject ownerObj;
    private IRifleSkill rifleSkill;

    public void Create(Pool onwerPool)
    {
        this.ownerPool = onwerPool;
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
        // todo ¿Ã∆Â∆Æ º“»Ø

        Collider[] objs = Physics.OverlapSphere(transform.position, 8f);

        foreach (Collider obj in objs)
        {
            EventBus_Damage.Publish(new DamageInfo(ownerObj, obj.gameObject, rifleSkill.Damage, null, DamageType.Damage));
        }

        ReturnToPool();
    }

    public void ReturnToPool()
    {
        ownerPool.ReturnToPool(gameObject);
    }
}
