using System;
using System.Collections.Generic;
using System.Linq;
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
    private Dictionary<int, EnemyData> enemyData = new();
    private Dictionary<int, ItemData> itemData = new();

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
            {
                weaponData.Add(row.id, row);
                itemData.Add(row.id, new WeaponData(row));
            }
                
            foreach (var row in originalData.ArmorData)
            {
                armorData.Add(row.id, row);
                itemData.Add(row.id, new ArmorData(row));
            }
                
            foreach (var row in originalData.HealkitData)
            {
                healkitData.Add(row.id, row);
                itemData.Add(row.id, new HealkitData(row));
            }                

            foreach (var row in originalData.UnitBaseStatsData)
                unitBaseStatsData.Add(row.id, row);

            foreach (var row in originalData.MonsterStatsData)
                monsterStatsData.Add(row.id, row);

            foreach (var row in originalData.PerkData)
                perkData.Add(row.id, row);

            //foreach (var row in originalData.AchievementsData)
            //    achievementsData.Add(row.id, row);

            //foreach (var row in originalData.LevelEXPData)
            //    levelExpData.Add(row.curLevel, row);

            // 추후 사용 시
            // foreach (var row in originalData.ClassStatsData)
            //     classStatsData.Add(row.id, row);

            foreach (var monsterData in monsterStatsData)
            {
                int monsterID = monsterData.Key;

                if (unitBaseStatsData.TryGetValue(monsterID, out var unitStats))
                {   
                    if(monsterID != unitStats.id)
                    {
                        Debug.Log("monster ID 불일치");
                        continue;
                    }
                    enemyData.Add(monsterID, new EnemyData(monsterData.Value, unitStats));
                }
                else
                {
                    Debug.LogWarning($"[EnemyData 누락] id {monsterID}에 해당하는 UnitBaseStatsData가 존재하지 않음");
                }
            }

            isLoadData = true;
        }
    }

    public List<WeaponData_Entity> GetWeaponList()
    {
        return weaponData.Values.ToList();
    }
    
    public bool GetWeaponData(int id, out WeaponData_Entity data)
    {
        return weaponData.TryGetValue(id, out data);
    }

    // 방어구 데이터
    public bool GetArmorData(int id, out ArmorData_Entity data)
    {
        return armorData.TryGetValue(id, out data);
    }

    // 힐킷 데이터
    public bool GetHealkitData(int id, out Healkit_Entity data)
    {
        return healkitData.TryGetValue(id, out data);
    }

    // 유닛 기본 스탯
    public bool GetUnitBaseStats(int id, out UnitBaseStats_Entity data)
    {
        return unitBaseStatsData.TryGetValue(id, out data);
    }

    // 몬스터(적) 스탯
    public bool GetEnemyStats(int id, out EnemyData data)
    {
        return enemyData.TryGetValue(id, out data);
    }

    // 특전 (Perk)
    public bool GetPerkData(int id, out PerkData_Entity data)
    {
        return perkData.TryGetValue(id, out data);
    }

    // 업적 데이터
    public bool GetAchievementData(int id, out AchievementsData_Entity data)
    {
        return achievementsData.TryGetValue(id, out data);
    }

    // 레벨 경험치 데이터
    public bool GetLevelExpData(int level, out LevelEXPData_Entity data)
    {
        return levelExpData.TryGetValue(level, out data);
    }
        
    public bool GetMonsterData(int id, out MonsterStats_Entity data)
    {
        return monsterStatsData.TryGetValue(id, out data);
    }
}

public class EnemyData
{
    public int id;
    public string name;
    public float maxHp;
    public float moveSpeed;
    public float damage;
    public string description;    
    public float attackSpeed;
    public float range;    

    public EnemyData(MonsterStats_Entity monsterData, UnitBaseStats_Entity unitBaseData)
    {
        id = monsterData.id;
        name = unitBaseData.name;
        maxHp = unitBaseData.maxHp;
        moveSpeed = unitBaseData.moveSpeed;
        damage = unitBaseData.damage;
        description = unitBaseData.description;
        attackSpeed = monsterData.attackSpeed;
        range = monsterData.range;
    }
}

public class ItemData
{
    public int itemID;
    public string name;
    public int price;
}

// 무기를 모든 직업이 다착용가능하지만 자기 직업 전용 무기에 특수효과 <<
// 상점에는 내가 시작할때 가지고 있는 무기의 레벨 이상의 모든 무기가 랜덤하게 생성(라이플, 샷건, 중화기)

public class WeaponData : ItemData
{
    public float damagePerShot;
    public float fireRate;
    public int ammoPerReload;
    public int maxAmmo;
    public float range;
    public float weight;
    public int weaponLevel;
    public WeaponData_Entity data;

    public WeaponData(WeaponData_Entity newData)
    {
        data = newData;
        itemID = newData.id;
        name = newData.name;//
        damagePerShot = newData.damagePerShot;//
        fireRate = newData.fireRate;//
        ammoPerReload = newData.ammoPerReload;//
        maxAmmo = newData.maxAmmo;//
        range = newData.range;
        weight = newData.weight;
        price = newData.price;//
        weaponLevel = newData.weaponLevel;//
    }
}


public class HealkitData : ItemData
{
    public float healAmount;    
    public HealkitData(Healkit_Entity newData)
    {
        itemID = newData.id;
        name = newData.name;
        healAmount = newData.healAmount;
        price = newData.price;
    }
}

public class ArmorData : ItemData
{
    public float durability;
    public float weight;
    public float damageRedcution;

    public ArmorData(ArmorData_Entity newData)
    {
        itemID = newData.id;
        name = newData.name;
        durability = newData.durability;
        weight = newData.weight;
        damageRedcution = newData.damageReduction;
        price = newData.price;
    }
}


