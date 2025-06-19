using UnityEngine;

[CreateAssetMenu(fileName = "RifleSkillData", menuName = "Scriptable Objects/ClassSkill/RifleSkillData")]
public class RifleSkillData : ClassSkillData, IRifleSkill
{
    [SerializeField] private int skillDamage;
    public int Damage => skillDamage;

    public override ClassSkill GetSkill()
    {
        return null;
    }
}
