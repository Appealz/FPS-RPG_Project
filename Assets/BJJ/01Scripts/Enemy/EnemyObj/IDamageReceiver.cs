using UnityEngine;

public interface IDamageReceiver
{
    // todo 데미지 구조체 만들 예정

    void OnHit(DamageReceivePart part, DamageInfo info);
    void OnExplosionDamageHandler(GameObject sender, DamageInfo info);
}

public enum DamageReceivePart
{
    Head,
    Body
}
