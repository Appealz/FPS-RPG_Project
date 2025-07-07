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
        // todo) 이속버프 구현하기
        EventBus_Damage.Publish(new DamageInfo(owner, owner, -survivorSkillData.HealPercent
                                , new MoveSpeedBuff(skillData.ID, skillData.name, survivorSkillData.Duration, survivorSkillData.IncreaseMoveSpeed,null, owner), DamageType.Heal));
        Debug.Log("Test Code - Survivor");
    }
}
