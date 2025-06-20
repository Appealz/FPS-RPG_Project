using UnityEngine;

[CreateAssetMenu(fileName = "SurvivorSkillData", menuName = "Scriptable Objects/ClassSkill/SurvivorSkillData")]
public class SurvivorSkillData : ClassSkillData, ISurvivorSkill
{
    [SerializeField] private float healPercent;
    [SerializeField] private float increaseMoveSpeed;
    [SerializeField] private float duration;

    public float HealPercent => healPercent;

    public float IncreaseMoveSpeed => increaseMoveSpeed;

    public float Duration => duration;

    public override ClassSkill GetSkill(GameObject newOwner)
    {
        return new SurvivorSkillLogic(newOwner, this);
    }
}
