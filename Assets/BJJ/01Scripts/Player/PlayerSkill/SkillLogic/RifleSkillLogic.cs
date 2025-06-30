using UnityEngine;

public class RifleSkillLogic : ClassSkill, IAnimSkill
{
    private IRifleSkill rifleSkillData;

    public RifleSkillLogic(GameObject newOwner, ClassSkillData data) : base(newOwner, data)
    {
        rifleSkillData = GetInterface<IRifleSkill>();
    }

    public void OnSkillAction()
    {
        Debug.Log("Test Code - Rifle Motion");
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
        Debug.Log("Test Code - Rifle");
    }
}
