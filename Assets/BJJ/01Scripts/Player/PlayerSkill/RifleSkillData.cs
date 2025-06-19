using UnityEngine;

[CreateAssetMenu(fileName = "RifleSkillData", menuName = "Scriptable Objects/ClassSkill/RifleSkillData")]
public class RifleSkillData : ClassSkillData, IRifleSkill
{
    public int Damage => throw new System.NotImplementedException();

    public override ClassSkill GetSkill()
    {
        return null;
    }
}
