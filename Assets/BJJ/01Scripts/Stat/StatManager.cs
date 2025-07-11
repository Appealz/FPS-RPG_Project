using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    HP,
    AttackDamage,
    MoveSpeed,
    AttackRange,
    AttackSpeed,
}

public enum StatEventType
{
    Add,
    Remove
}

public class StatModifier
{
    public GameObject sender;
    public StatEventType EventType;
    public StatType ChangedStatType;
    public float ChangeValue;
    public bool isMulti;

    public StatModifier(GameObject newSender,StatEventType eventType,StatType changedStatType, float changeValue, bool isMulti = false)
    {
        sender = newSender;
        EventType = eventType;
        ChangedStatType = changedStatType;
        ChangeValue = changeValue;
        this.isMulti = isMulti;
    }
}

[Serializable]
public class StatManager
{
    private GameObject owner;
    private Dictionary<StatType, StatValue> statMap = new Dictionary<StatType, StatValue>();
    private Dictionary<StatType, float> prevMap = new Dictionary<StatType, float>();

    [SerializeField] private float curHP;
    public int CurHP => Mathf.RoundToInt(curHP); // 내부처리는 curHP로 한 뒤 UI에 던지는 용도

    public ArmorManager armorManager { get; private set; }

    private List<Buff> activeBuffList;
    private List<IHpEffectModifier> activeHPModifier;

    public StatManager(GameObject newOwner, Dictionary<StatType, StatValue> statMap, List<Buff> setPerks = null)
    {
        owner = newOwner;
        this.statMap = statMap;

        curHP = statMap[StatType.HP].Value;
        armorManager = new ArmorManager();
        activeBuffList = setPerks ?? new List<Buff>();
        activeHPModifier = new List<IHpEffectModifier>();

        foreach (var buff in activeBuffList)
        {
            buff.OnApply();
        }

        foreach (var stat in this.statMap)
        {
            prevMap[stat.Key] = stat.Value.Value;
        }
    }

    public void StatUpdate()
    {
        float deltaTime = Time.deltaTime;
        bool isChange = false;

        for (int i = activeBuffList.Count - 1; i >= 0; i--)
        {
            activeBuffList[i].BuffUpdate(deltaTime);

            if (activeBuffList[i].isExpired)
            {
                isChange = true;
                activeBuffList[i].OnRemove();
                activeBuffList.RemoveAt(i);
            }
        }

        if(isChange)
            RecalculateStat();
    }

    private void RecalculateStat()
    {
        foreach(var stat in statMap)
        {
            if (stat.Value.HasChange(prevMap[stat.Key]))
            {
                // todo UI 변동
            }
        }
    }

    public void AddModifier(StatType type,float value, bool isMulti = false)
    {
        if(statMap.TryGetValue(type, out var stat))
        {
            stat.AddModifier(value, isMulti);
        }
    }

    public void RemoveModifier(StatType type, float value, bool isMulti = false)
    {
        if (statMap.TryGetValue(type, out var stat))
        {
            stat.RemoveModifier(value, isMulti);
        }
    }

    public void ApplyDamage(float damage) // 상위 매니저에서 끌어다가 사용할 예정
    {
        DamageResult(damage);
    }

    public void ApplyDamage(IHpEffectCalculator calculator)
    {
        float absorb = calculator.Calculate(this);
        DamageResult(absorb);
    }

    private void DamageResult(float absorb)
    {
        curHP -= armorManager.ApplyDamage(CurHPModifierCalculate(DamageType.Damage, absorb));
        if (curHP <= 0)
        {
            curHP = 0;
            Debug.Log("DieEvent");
            // todo 사망처리
        }

        // todo UI처리
    }

    public void ApplyHeal(float heal)
    {
        HealResult(heal);
    }

    public void ApplyHeal(IHpEffectCalculator calculator)
    {
        float absorb = calculator.Calculate(this);
        HealResult(absorb);
    }

    private void HealResult(float absorb)
    {
        curHP += CurHPModifierCalculate(DamageType.Heal, absorb);
        if (curHP >= statMap[StatType.HP].Value)
            curHP = statMap[StatType.HP].Value;

        // todo) UI처리
    }

    private float CurHPModifierCalculate(DamageType type, float absorb)
    {
        float result = absorb;
        for(int i = activeHPModifier.Count - 1; i >= 0; i--)
        {
            result = activeHPModifier[i].Modify(type,result);
        }

        return result;
    }

    public void AddBuff(Buff newBuff)
    {
        newBuff.OnApply();
        activeBuffList.Add(newBuff);
    }

    public void AddHPModifier(IHpEffectModifier modifier)
    {
        activeHPModifier.Add(modifier);
    }

    public void RemoveModifier(IHpEffectModifier modifier)
    {
        activeHPModifier.Remove(modifier);
    }

    public float GetStat(StatType type)
    {
        if(statMap.TryGetValue(type, out var stat))
        {
            return stat.Value;
        }

        Debug.Log($"StatManager.cs - GetStat() - {type} Type Stat Not Have");
        return -1;
    }

    public int GetStatInt(StatType type)
    {
        return Mathf.RoundToInt(GetStat(type));
    }
}
