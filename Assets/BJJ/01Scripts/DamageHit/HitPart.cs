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

// ������ �̺�Ʈ�� ���� ��������?
// ������ / ���� / ������ / �۽���