using UnityEngine;

public interface IHpEffectCalculator
{
    float Calculate(StatManager receiver);
}

public class MaxHPRatioCalculator : IHpEffectCalculator
{
    private float ratio;
    private float value;

    public MaxHPRatioCalculator(float ratio, float value)
    {
        this.ratio = ratio;
        this.value = value;
    }

    public float Calculate(StatManager receiver)
    {
        return value + (receiver.GetStat(StatType.HP) * ratio);
    }
}