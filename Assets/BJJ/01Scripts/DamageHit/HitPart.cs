using UnityEngine;

public interface IHitPart
{
    IDamageReceiver owner { get; }
    void Init(IDamageReceiver owner);
    void DamageEventHandler(DamageInfo info);
}

public enum HitEffectType
{
    Flesh,
    Metal,
    Concrete,
    Wood,
    Glass
}

public class HitPart : MonoBehaviour, IHitPart
{
    public IDamageReceiver owner { get; private set; }
    private DamageReceivePart part;
    private HitEffectType efxType;

    public void Init(IDamageReceiver owner)
    {
        this.owner = owner;
        if (name.Contains("head") || name.Contains("Head"))
            part = DamageReceivePart.Head;
        else part = DamageReceivePart.Body;

        switch(this.owner.ReciverGO.layer)
        {
            case int layer when layer == LayerMask.NameToLayer("Player"):
                efxType = HitEffectType.Flesh;
                break;
            case int layer when layer == LayerMask.NameToLayer("Enemy"):
                efxType = HitEffectType.Metal;
                break;
        }

        EventBus_Damage.SubScribe(DamageEventHandler);
    }
    public void DamageEventHandler(DamageInfo info)
    {
        if (info.receiver != gameObject) return;

        if(info.hitPos != default)
            HitEffect(info.hitPos);
        owner.OnHit(part, info);
    }

    private void HitEffect(Vector3 hitPos)
    {
        GameObject efx = null;
        switch(efxType)
        {
            case HitEffectType.Flesh:
                efx = PoolManager.Instance.GetPool("FleshEffect").GetObjFromPool();
                break;
            case HitEffectType.Metal:
                efx = PoolManager.Instance.GetPool("HitEffect").GetObjFromPool();
                break;
            case HitEffectType.Concrete:
                break;
            case HitEffectType.Wood:
                break;
            case HitEffectType.Glass:
                break;
        }

        efx.transform.position = hitPos;
        if (efx.TryGetComponent<IEffectObject>(out var effect))
            effect.EffectStart();
    }

    private void OnDisable()
    {
        EventBus_Damage.UnSubscribe(DamageEventHandler);
    }
}

// 데미지 이벤트에 뭐가 들어가야하죠?
// 데미지 / 버프 / 수신자 / 송신자