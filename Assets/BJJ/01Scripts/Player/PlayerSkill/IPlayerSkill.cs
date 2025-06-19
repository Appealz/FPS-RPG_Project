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
    private float curCoolDown;

    public ClassSkill(ClassSkillData data)
    {
        skillData = data;
        curCoolDown = 0f;
    }

    public bool IsUseable => curCoolDown <= 0f;

    public abstract void UseSkill();
}

public abstract class ClassSkillData : ScriptableObject
{
    public int ID;
    public float CoolDown;
    public string Description;

    public abstract ClassSkill GetSkill();
}

public interface IDurationSkill
{
    float Duration { get; }
}


public interface IRifleSkill
{
    int Damage { get; }
}

public interface IShotgunSkill : IDurationSkill
{
    float ReduceAttackSpeed { get; }
}

public interface ISurvivorSkill : IDurationSkill
{
    float Healpercent { get; }
    float IncreaseMoveSpeed { get; }
}

public interface IEngineerSkill : IDurationSkill
{
    int SentryGunMaxHP { get; }
    int SentryGunAttackDamage { get; }
    float SentryGunAttackSpeed { get; }
}