using UnityEngine;

public interface IDamageReceiver
{
    GameObject ReciverGO { get; }
    void OnHit(DamageReceivePart part, DamageInfo info);
    void OnExplosionDamageHandler(DamageInfo info);
}

public enum DamageReceivePart
{
    Head,
    Body
}
