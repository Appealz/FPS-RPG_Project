using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private bool isSpawnState;
    private float spawnDelay;
    private float curTime;
    private int multiSpawnCount;

    private RoundData roundData;
    private Dictionary<int, SpawnUnit> countMap;
    private Dictionary<int, SpawnUnit> dataMap;

    public void SpawnUpdate()
    {
        if (!isSpawnState) return;

        curTime += Time.deltaTime;

        if(curTime >= spawnDelay)
        {
            for (int i = 0; i < multiSpawnCount; i++)
            {
                Spawn();
            }
            curTime = 0;
        }
    }

    public void SetSpawnData(RoundData data)
    {
        roundData = data;
        multiSpawnCount = roundData.curRound + 1;
        countMap = new Dictionary<int, SpawnUnit>();
        dataMap = new Dictionary<int, SpawnUnit>();

        foreach(var unit in roundData.spawnList)
        {
            countMap[unit.id] = new SpawnUnit {id = unit.id, Name = unit.Name, Count = 0 };
            dataMap[unit.id] = unit;
        }

        isSpawnState = true;
        curTime = 0f;
    }

    private void Spawn()
    {
        var validNames = countMap.Where(kv => kv.Value.Count < dataMap[kv.Key].Count)
            .Select(kv => kv.Key).ToList();

        if(validNames.Count == 0)
        {
            isSpawnState = false;
            return;
        }

        int pick = validNames[Random.Range(0, validNames.Count)];
        if(EnemySpawn(pick))
        {
            var temp = countMap[pick];
            temp.Count++;
            countMap[pick] = temp;
        }
    }

    private bool EnemySpawn(int id)
    {
        if(DataManager.Instance.GetEnemyStats(id, out var data))
        {
            GameObject obj = PoolManager.Instance.GetPool(data.name).GetObjFromPool();
            if (obj.TryGetComponent<EnemyManager>(out var enemy))
            {
                enemy.InitEnemy(data);
                // 위치 랜덤으로 던지기

                return true;
            }

            Debug.Log($"EnemySpawnManager.cs - EnemySpawn() - Can't Find EnemyManager {id}");
            return false;
        }
        Debug.Log($"EnemySpawnManager.cs - EnemySpawn() - Can't Find EnemyData {id}");
        return false;
    }
}
