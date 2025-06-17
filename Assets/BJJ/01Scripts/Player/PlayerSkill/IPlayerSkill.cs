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
    public abstract void UseSkill();
}