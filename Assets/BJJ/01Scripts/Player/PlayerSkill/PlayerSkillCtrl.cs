using UnityEngine;

public class PlayerSkillCtrl : MonoBehaviour, IPlayerSkill
{
    private ClassSkill curSkill;
    private bool isUseSkill;

    public void InitSkillCtrl(ClassSkill newSkill)
    {
        curSkill = newSkill;
        isUseSkill = false;
    }

    public void SetEnable(bool isOn)
    {
        isUseSkill = isOn;
    }

    public void UpdateSkillCtrl()
    {
        if (!isUseSkill) return;

        curSkill.UseSkill();
    }
}
