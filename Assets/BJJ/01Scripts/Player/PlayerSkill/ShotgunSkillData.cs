using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunSkillData", menuName = "Scriptable Objects/ClassSkill/ShotgunSkillData")]
public class ShotgunSkillData : ClassSkillData, IShotgunSkill
{
    [SerializeField] private float reduceAttackSpeed;
    [SerializeField] private float duration;

    public float ReduceAttackSpeed => reduceAttackSpeed;

    public float Duration => duration;

    public override ClassSkill GetSkill(GameObject newOwner)
    {
        return new ShotgunSkillLogic(newOwner, this);
    }
}
