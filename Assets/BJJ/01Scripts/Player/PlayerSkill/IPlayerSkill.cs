using UnityEngine;

public interface IPlayerSkill
{
    /// <summary>
    ///  ½ºÅ³ 
    /// </summary>
    void InitSkillCtrl(ClassSkill newSkill);
    void UpdateSkillCtrl();

    void SetEnable(bool isOn);
}

public abstract class ClassSkill
{
    private ClassSkillData skillData;
    public ClassSkill(ClassSkillData data)
    {
        skillData = data;
    }

    public abstract void UseSkill();
}

public abstract class ClassSkillData : ScriptableObject
{
    public int ID;
    public float CoolDown;

    public abstract ClassSkill GetSkill();
}