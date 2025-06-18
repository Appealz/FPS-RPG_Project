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

    // todo 버프 시스템 구현 후 버프리스트 관리할 예정

    public StatManager(Dictionary<StatType, StatValue> statMap)
    {
        this.statMap = statMap;
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
}
