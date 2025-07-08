using UnityEngine;

public interface IHpEffectModifier
{
    float Modify(DamageType type,float rawValue);
}

