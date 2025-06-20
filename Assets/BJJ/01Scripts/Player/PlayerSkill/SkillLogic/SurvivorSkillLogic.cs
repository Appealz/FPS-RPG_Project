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
        Debug.Log("Test Code - Survivor");
    }
}
