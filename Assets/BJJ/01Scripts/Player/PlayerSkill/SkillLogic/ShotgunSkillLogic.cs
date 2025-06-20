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
            Debug.Log($"�۵� �Ұ� - ��Ÿ�� {curCoolDown}");
            return;
        }

        curCoolDown = skillData.CoolDown;
        StartCooldown();
        // ���� �ִϸ��̼� �̺�Ʈ�� ����Ͽ� ��ų �۵� ����
        Debug.Log("Test Code - Shotgun");
    }
}
