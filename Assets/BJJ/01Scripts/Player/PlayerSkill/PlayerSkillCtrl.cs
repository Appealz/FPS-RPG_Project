using UnityEngine;

public class PlayerSkillCtrl : MonoBehaviour, IPlayerSkill
{
    private ClassSkill curSkill;
    private bool isUseSkill;

    public void InitSkillCtrl(ClassSkillData data)
    {
        curSkill = data.GetSkill(gameObject);
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
