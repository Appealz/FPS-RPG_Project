using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName = "Scriptable Objects/SpawnData")]
public class SpawnData : ScriptableObject
{
    public List<RoundData> RoundEnemyDataList;
}

[Serializable]
public class RoundData
{
    public int curRound;
    public List<SpawnUnit> spawnList;
}

[Serializable]
public struct SpawnUnit
{
    public int id;
    public string Name;
    public int Count;
}