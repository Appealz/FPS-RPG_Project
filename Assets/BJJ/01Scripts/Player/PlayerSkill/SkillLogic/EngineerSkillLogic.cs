using UnityEngine;

public class EngineerSkillLogic : ClassSkill
{
    private IEngineerSkill engineerSkillData;

    public EngineerSkillLogic(GameObject newOwner, ClassSkillData data) : base(newOwner, data)
    {
        engineerSkillData = GetInterface<IEngineerSkill>();
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
        Debug.Log("Test Code - Engineer");
    }
}
