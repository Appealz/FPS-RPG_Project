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
        // todo �ٵ� �׳� ����Ʈ�� �۵���Ű�� �۵����ѵ� ���� ������?
        EventBus_Damage.Publish(new DamageInfo(owner, owner, 0
                                , new MoveSpeedBuff(skillData.ID, skillData.name, survivorSkillData.Duration, survivorSkillData.IncreaseMoveSpeed,null, owner), DamageType.Heal
                                , HPCalculateType.Calculator, new MaxHPRatioCalculator(survivorSkillData.HealPercent, 0)));
        Debug.Log("Test Code - Survivor");
    }
}
