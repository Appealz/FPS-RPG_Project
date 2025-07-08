using UnityEngine;

public class SurvivorSkillLogic : ClassSkill
{
    private ISurvivorSkill survivorSkillData;
    public SurvivorSkillLogic(GameObject newOwner, ClassSkillData data) : base(newOwner, data)
    {
        survivorSkillData = GetInterface<ISurvivorSkill>();
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
        // 추후 애니메이션 이벤트에 기반하여 스킬 작동 예정
        // todo 근데 그냥 이펙트만 작동시키고 작동시켜도 문제 없을듯?
        EventBus_Damage.Publish(new DamageInfo(owner, owner, 0
                                , new MoveSpeedBuff(skillData.ID, skillData.name, survivorSkillData.Duration, survivorSkillData.IncreaseMoveSpeed,null, owner), DamageType.Heal
                                , HPCalculateType.Calculator, new MaxHPRatioCalculator(survivorSkillData.HealPercent, 0)));
        Debug.Log("Test Code - Survivor");
    }
}
