using System;
using UnityEngine;

// 테이블에 있는 데이터들의 타입을 지정. (매칭 하는 클래스)
[Serializable]
public class WeaponData_Entity
{
    public int id;
    public string name;
    public float damagePerShot;
    public float fireRate;
    public int ammoPerReload;
    public int maxAmmo;
    public float range;
    public float weight;
    public int price;
    public int weaponLevel;
    public string weaponType;
}

[Serializable]
public class ArmorData_Entity
{
    public int id;
    public string name;
    public float durability;
    public float weight;
    public float damageReduction;
    public int price;
}

[Serializable]
public class Healkit_Entity
{
    public int id;
    public string name;
    public float healAmount;
    public int price;
}

[Serializable]
public class UnitBaseStats_Entity
{
    public int id;
    public string name;
    public float maxHp;
    public float moveSpeed;
    public float damage;
    public string description;
}

[Serializable]
public class MonsterStats_Entity
{
    public int id;    
    public float attackSpeed;
    public float range;
}

//[Serializable]
//public class ClassStatsData_Entity
//{

//}

[Serializable]
public class PerkData_Entity
{
    public int id;
    public string name;
    public string description;
}

public class AchievementsData_Entity
{
    public int id;
    public string name;
    public string description;
}

public class LevelEXPData_Entity
{
    public int curLevel;
    public int nextEXP;
}

