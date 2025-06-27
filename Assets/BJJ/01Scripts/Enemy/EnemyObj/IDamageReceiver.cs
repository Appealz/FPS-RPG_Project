using UnityEngine;

public interface IDamageReceiver
{
    // todo ������ ����ü ���� ����

    void OnHit(DamageReceivePart part, DamageInfo info);
    void OnExplosionDamageHandler(GameObject sender, DamageInfo info);
}

public enum DamageReceivePart
{
    Head,
    Body
}
