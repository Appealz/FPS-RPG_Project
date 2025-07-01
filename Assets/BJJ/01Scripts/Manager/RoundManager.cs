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

        // todo ���ؽ�Ʈ�� �����ؼ� �ش� ���̵��� Ȯ���� �� ���̵��� �´� �����͸� ������ ����
        // ������ �׽�Ʈ�ڵ�
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
