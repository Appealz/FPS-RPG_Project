using UnityEngine;

public class RifleSkillLogic : ClassSkill
{
    public RifleSkillLogic(ClassSkillData data) : base(data)
    {

    }

    public override void UseSkill()
    {
        if (!IsUseable) return;

        Debug.Log("Test Code");
        curCoolDown = skillData.CoolDown;
        // todo UniTask ����Ʈ �� �񵿱� ó��
    }
}
