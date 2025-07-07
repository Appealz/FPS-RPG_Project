using UnityEngine;

public class MoveSpeedBuff : Buff
{
    private float amount;

    public MoveSpeedBuff(int buffID, string buffName, float duration, float amount,Sprite buffIcon, GameObject target, bool isStackable = false) : base(buffID, buffName, duration, buffIcon, target, isStackable)
    {
        this.amount = amount;
    }

    public override void OnApply()
    {
        EventBus_Stat.Publish(new StatModifier(Target, StatEventType.Add, StatType.MoveSpeed, amount, true));
    }

    public override void OnRemove()
    {
        EventBus_Stat.Publish(new StatModifier(Target, StatEventType.Remove, StatType.MoveSpeed, amount, true));
    }
}
