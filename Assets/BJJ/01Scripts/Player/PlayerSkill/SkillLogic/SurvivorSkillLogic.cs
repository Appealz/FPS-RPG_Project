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
            Debug.Log($"�۵� �Ұ� - ��Ÿ�� {curCoolDown}");
            return;
        }

        curCoolDown = skillData.CoolDown;
        StartCooldown();
        // ���� �ִϸ��̼� �̺�Ʈ�� ����Ͽ� ��ų �۵� ����
        Debug.Log("Test Code - Survivor");
    }
}
