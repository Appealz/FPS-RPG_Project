using UnityEngine;

public class AttackSpeedBuff : Buff
{
    private float amount;

    public AttackSpeedBuff(int buffID, string buffName, float duration, Sprite buffIcon, GameObject target, float amount,bool isStackable = false) : base(buffID, buffName, duration, buffIcon, target, isStackable)
    {
        this.amount = amount;
    }

    // todo UI적인 부분도 여기서 처리하면 될듯?

    public override void OnApply()
    {
        EventBus_Stat.Publish(new StatModifier(Target, StatEventType.Add, StatType.AttackSpeed, amount, true));
    }

    public override void OnRemove()
    {
        EventBus_Stat.Publish(new StatModifier(Target, StatEventType.Remove, StatType.AttackSpeed, amount, true));
    }
}
