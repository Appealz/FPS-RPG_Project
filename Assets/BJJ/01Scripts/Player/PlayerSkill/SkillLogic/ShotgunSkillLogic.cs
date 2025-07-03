using UnityEngine;

public class ShotgunSkillLogic : ClassSkill
{
    private IShotgunSkill shotgunSkillData;

    public ShotgunSkillLogic(GameObject newOwner, ClassSkillData data) : base(newOwner, data)
    {
        shotgunSkillData = GetInterface<ShotgunSkillData>();
    }

    public override void UseSkill()
    {
        if (!IsUseable)
        {
            Debug.Log($"작동 불가 - 쿨타임 {curCoolDown}");
            return;
        }

        curCoolDown = skillData.CoolDown;
        StartCooldown();

        // todo Effect
        EventBus_Buff.Publish(new BuffEvent(BuffEventType.Add, owner, owner, 
                                            new AttackSpeedBuff(skillData.ID, skillData.name, shotgunSkillData.Duration, null, owner, shotgunSkillData.ReduceAttackSpeed)));
        Debug.Log("Test Code - Shotgun");
    }
}
