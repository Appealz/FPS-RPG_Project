using UnityEngine;

public class PlayerSkillCtrl : MonoBehaviour, IPlayerSkill
{
    private ClassSkill curSkill;
    private bool isUseSkill;
    private bool isSkillAction;

    public void InitSkillCtrl(ClassSkillData data)
    {
        curSkill = data.GetSkill(gameObject);
        isUseSkill = false;
        isSkillAction = false;
    }

    public void OnSkillActionHandler()
    {
        if(curSkill is IAnimSkill animSkill)
        {
            animSkill.OnSkillAction();
        }
    }

    public void SetEnable(bool isOn)
    {
        isUseSkill = isOn;
        isSkillAction = false;
    }

    public void UpdateSkillCtrl()
    {
        if (!isUseSkill) return;
        if (isSkillAction) return;

        isSkillAction = true;
        curSkill.UseSkill();
    }

    
}
