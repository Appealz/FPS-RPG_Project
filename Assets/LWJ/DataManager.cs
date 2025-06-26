using System.Collections.Generic;
using UnityEngine;

public class DataManager : DontDestroySingleton<DataManager>
{
    private TableData originalData;
    private bool isLoadData = false;
    private Dictionary<int, WeaponData_Entity> weaponData = new();
    private Dictionary<int, ArmorData_Entity> armorData = new();
    private Dictionary<int, Healkit_Entity> healkitData = new();
    private Dictionary<int, UnitBaseStats_Entity> unitBaseStatsData = new();
    private Dictionary<int, MonsterStats_Entity> monsterStatsData = new();
    private Dictionary<int, PerkData_Entity> perkData = new();
    private Dictionary<int, AchievementsData_Entity> achievementsData = new();
    private Dictionary<int, LevelEXPData_Entity> levelExpData = new();
    //private Dictionary<int, ClassStatsData_Entity> classStatsData = new();

    protected override void DoAwake()
    {
        base.DoAwake();
        originalData = Resources.Load<TableData>("TableData");
        LoadData();
    }

    private void LoadData()
    {
        if(!isLoadData)
        {
            foreach (var row in originalData.WeaponData)
                weaponData.Add(row.id, row);

            foreach (var row in originalData.ArmorData)
                armorData.Add(row.id, row);

            foreach (var row in originalData.HealkitData)
                healkitData.Add(row.id, row);

            foreach (var row in originalData.UnitBaseStatsData)
                unitBaseStatsData.Add(row.id, row);

            foreach (var row in originalData.MonsterStatsData)
                monsterStatsData.Add(row.id, row);

            foreach (var row in originalData.PerkData)
                perkData.Add(row.id, row);

            foreach (var row in originalData.AchievementsData)
                achievementsData.Add(row.id, row);

            foreach (var row in originalData.LevelEXPData)
                levelExpData.Add(row.curLevel, row);

            // ���� ��� ��
            // foreach (var row in originalData.ClassStatsData)
            //     classStatsData.Add(row.id, row);

            isLoadData = true;
        }
    }

    public bool GetWeaponData(int id, out WeaponData_Entity data)
    {
        return weaponData.TryGetValue(id, out data);
    }

    // �� ������
    public bool GetArmorData(int id, out ArmorData_Entity data)
    {
        return armorData.TryGetValue(id, out data);
    }

    // ��Ŷ ������
    public bool GetHealkitData(int id, out Healkit_Entity data)
    {
        return healkitData.TryGetValue(id, out data);
    }

    // ���� �⺻ ����
    public bool GetUnitBaseStats(int id, out UnitBaseStats_Entity data)
    {
        return unitBaseStatsData.TryGetValue(id, out data);
    }

    // ���� ����
    public bool GetMonsterStats(int id, out MonsterStats_Entity data)
    {
        return monsterStatsData.TryGetValue(id, out data);
    }

    // Ư�� (Perk)
    public bool GetPerkData(int id, out PerkData_Entity data)
    {
        return perkData.TryGetValue(id, out data);
    }

    // ���� ������
    public bool GetAchievementData(int id, out AchievementsData_Entity data)
    {
        return achievementsData.TryGetValue(id, out data);
    }

    // ���� ����ġ ������
    public bool GetLevelExpData(int level, out LevelEXPData_Entity data)
    {
        return levelExpData.TryGetValue(level, out data);
    }
}

