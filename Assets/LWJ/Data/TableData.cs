using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources")]
public class TableData : ScriptableObject
{
	public List<WeaponData_Entity> WeaponData; // Replace 'EntityType' to an actual type that is serializable.
	public List<ArmorData_Entity> ArmorData; // Replace 'EntityType' to an actual type that is serializable.
	public List<Healkit_Entity> HealkitData; // Replace 'EntityType' to an actual type that is serializable.
	public List<UnitBaseStats_Entity> UnitBaseStatsData; // Replace 'EntityType' to an actual type that is serializable.
	public List<MonsterStats_Entity> MonsterStatsData; // Replace 'EntityType' to an actual type that is serializable.
	//public List<ClassStatsData_Entity> ClassStatsData; // Replace 'EntityType' to an actual type that is serializable.
	public List<PerkData_Entity> PerkData; // Replace 'EntityType' to an actual type that is serializable.
	//public List<AchievementsData_Entity> AchievementsData; // Replace 'EntityType' to an actual type that is serializable.
	public List<LevelEXPData_Entity> LevelEXPData; // Replace 'EntityType' to an actual type that is serializable.
}

