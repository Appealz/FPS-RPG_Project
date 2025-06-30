using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerSkill
{
    /// <summary>
    ///  ½ºÅ³ 
    /// </summary>
    void InitSkillCtrl(ClassSkillData newData);
    void UpdateSkillCtrl();

    void OnSkillActionHandler();

    void SetEnable(bool isOn);
}

public interface IAnimSkill
{
    void OnSkillAction();
}

public abstract class ClassSkill
{
    protected GameObject owner;
    protected ClassSkillData skillData;
    protected float curCoolDown;

    public ClassSkill(GameObject newOwner, ClassSkillData data)
    {
        owner = newOwner;
        skillData = data;
        curCoolDown = 0f;
    }

    public bool IsUseable => curCoolDown <= 0f;

    public abstract void UseSkill();

    protected async void StartCooldown()
    {
        curCoolDown = skillData.CoolDown;
        Debug.Log($"CoolDown_Start {curCoolDown}S");

        while (curCoolDown > 0f)
        {
            await UniTask.Yield();
            curCoolDown -= Time.deltaTime;
        }

        curCoolDown = 0f;
        Debug.Log("CoolDown_End");
    }

    private Dictionary<Type, object> _interfaceCache = new();
    protected T GetInterface<T>() where T : class
    {
        var type = typeof(T);
        if (!_interfaceCache.TryGetValue(type, out var cache))
        {
            cache = skillData as T;
            _interfaceCache[type] = cache;
        }

        return cache as T;
    }
}

public abstract class ClassSkillData : ScriptableObject
{
    public int ID;
    public float CoolDown;
    public string Description;

    public abstract ClassSkill GetSkill(GameObject newOwner);
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
    float HealPercent { get; }
    float IncreaseMoveSpeed { get; }
}

public interface IEngineerSkill : IDurationSkill
{
    int SentryGunMaxHP { get; }
    int SentryGunAttackDamage { get; }
    float SentryGunAttackSpeed { get; }
}