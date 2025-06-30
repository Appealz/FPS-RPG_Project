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
            Debug.Log($"�۵� �Ұ� - ��Ÿ�� {curCoolDown}");
            return;
        }

        curCoolDown = skillData.CoolDown;
        StartCooldown();
        // ���� �ִϸ��̼� �̺�Ʈ�� ����Ͽ� ��ų �۵� ����
        Debug.Log("Test Code - Rifle");
    }
}
