using UnityEngine;

public interface IHitPart
{
    void Init(IDamageReceiver owner, DamageReceivePart part);
    void DamageEventHandler(DamageInfo info);
}

public class HitPart : MonoBehaviour, IHitPart
{
    private IDamageReceiver owner;
    private DamageReceivePart part;

    public void Init(IDamageReceiver owner, DamageReceivePart part)
    {
        this.owner = owner;
        this.part = part;

        EventBus_Damage.SubScribe(DamageEventHandler);
    }
    public void DamageEventHandler(DamageInfo info)
    {
        if (info.receiver != gameObject) return;

        owner.OnHit(part, info);
    }

    private void OnDisable()
    {
        EventBus_Damage.UnSubscribe(DamageEventHandler);
    }
}

// 데미지 이벤트에 뭐가 들어가야하죠?
// 데미지 / 버프 / 수신자 / 송신자