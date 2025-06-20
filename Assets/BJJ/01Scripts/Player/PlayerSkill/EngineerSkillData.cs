using UnityEngine;

[CreateAssetMenu(fileName = "EngineerSkillData", menuName = "Scriptable Objects/ClassSkill/EngineerSkillData")]
public class EngineerSkillData : ClassSkillData, IEngineerSkill
{
    [SerializeField] private int sentryMaxHP;
    [SerializeField] private int sentryAttackDamage;
    [SerializeField] private float sentryAttackSpeed;
    [SerializeField] private float sentryDuration;

    public int SentryGunMaxHP => sentryMaxHP;

    public int SentryGunAttackDamage => sentryAttackDamage;

    public float SentryGunAttackSpeed => sentryAttackSpeed;

    public float Duration => sentryDuration;

    public override ClassSkill GetSkill(GameObject newOwner)
    {
        return new EngineerSkillLogic(newOwner, this);
    }
}
