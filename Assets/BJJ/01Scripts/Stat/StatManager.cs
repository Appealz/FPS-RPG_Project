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

public class StatManager
{
    private Dictionary<StatType, StatValue> statMap = new Dictionary<StatType, StatValue>();
    private Dictionary<StatType, float> prevMap = new Dictionary<StatType, float>();

    private float curHP;
    public int CurHP => Mathf.RoundToInt(curHP); // 내부처리는 curHP로 한 뒤 UI에 던지는 용도

    private ArmorManager armorManager;

    private List<Buff> activeBuffList;

    public StatManager(Dictionary<StatType, StatValue> statMap, List<Buff> setPerks = null)
    {
        this.statMap = statMap;

        curHP = statMap[StatType.HP].Value;
        armorManager = new ArmorManager();
        activeBuffList = setPerks ?? new List<Buff>();

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
        float absorb = armorManager.ApplyDamage(damage);
        curHP -= absorb;

        if(curHP <= 0)
        {
            curHP = 0;
            Debug.Log("DieEvent");
            // todo 사망처리
        }

        // todo UI처리
    }

    private float GetStat(StatType type)
    {
        if(statMap.TryGetValue(type, out var stat))
        {
            return stat.Value;
        }

        Debug.Log($"StatManager.cs - GetStat() - {type} Type Stat Not Have");
        return -1;
    }

    private int GetStatInt(StatType type)
    {
        return Mathf.RoundToInt(GetStat(type));
    }
}
