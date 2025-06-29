using UnityEngine;

public interface IHitPart
{
    IDamageReceiver owner { get; }
    void Init(IDamageReceiver owner);
    void DamageEventHandler(DamageInfo info);
}

public class HitPart : MonoBehaviour, IHitPart
{
    public IDamageReceiver owner { get; private set; }
    private DamageReceivePart part;

    public void Init(IDamageReceiver owner)
    {
        this.owner = owner;
        if (name.Contains("head") || name.Contains("Head"))
            part = DamageReceivePart.Head;
        else part = DamageReceivePart.Body;

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