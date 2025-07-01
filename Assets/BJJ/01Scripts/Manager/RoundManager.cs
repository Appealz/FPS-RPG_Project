using System;
using System.Linq;
using UnityEngine;

public class RoundManager
{
    private int curRound = 0;
    private int defeatedEnemy = 0;
    private int roundTotalEnemy;

    private SpawnData spawnData;

    public Action OnRoundStart;
    public Action OnRoundEnd;

    public bool IsFinalRound
    {
        get
        {
            if (spawnData == null) return false;
            return curRound >= spawnData.RoundEnemyDataList.Count;
        }
    }

    public void InitRoundManager()
    {
        curRound = 0;

        // todo 컨텍스트에 접근해서 해당 난이도를 확인한 뒤 난이도에 맞는 데이터를 가져올 예정
        // 지금은 테스트코드
        spawnData = Resources.Load<SpawnData>("SpawnData/Test");

        EnemyTotalManager.Instance.OnEnemyDeadEvent += RegisterEnemyDeath;
    }

    public void StartRound()
    {
        curRound++;
        defeatedEnemy = 0;

        RoundData roundData = spawnData.RoundEnemyDataList.Where(d => d.curRound == curRound).FirstOrDefault();
        if(roundData == null)
        {
            Debug.Log("RoundManager.cs - StartRound - NonRoundData");
            return;
        }

        roundTotalEnemy = roundData.spawnList.Sum(data => data.Count);
        EnemyTotalManager.Instance.SetSpawnData(roundData);
        OnRoundStart?.Invoke();
    }

    private void RegisterEnemyDeath()
    {
        defeatedEnemy++;

        if(defeatedEnemy >= roundTotalEnemy)
        {
            OnRoundEnd?.Invoke();
        }
    }

    public void DisableRoundManager()
    {
        EnemyTotalManager.Instance.OnEnemyDeadEvent -= RegisterEnemyDeath;
    }
}
