using System;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyUpdateType
{ 
    Regist,
    Unregist
}

public struct EnemyUpdateEvent
{
    public EnemyUpdateType type;
    public IEnemyManager enemy;

    public EnemyUpdateEvent(EnemyUpdateType type, IEnemyManager enemy)
    {
        this.type = type;
        this.enemy = enemy;
    }
}

[RequireComponent(typeof(EnemySpawnManager))]
public class EnemyTotalManager : DestroySingleton<EnemyTotalManager>
{
    private EnemySpawnManager spawnManager;
    private List<IEnemyManager> enemies;

    public static event Action OnEnemyDeadEvent;
    
    protected override void DoAwake()
    {
        if (!TryGetComponent(out spawnManager))
            Debug.Log("EnemyTotalManager.cs - DoAwake() - SpawnManager Can't Find");
    }

    public void InitEnemyManager()
    {
        enemies = new List<IEnemyManager>();

        GameManager.OnGameUpdate += EnemyUpdate;
        EventBus_EnemyManager.Subscribe(EnemyListUpdateHandler);
    }

    public void SetSpawnData(RoundData data)
    {
        spawnManager.SetSpawnData(data);
    }

    private void OnDisable()
    {
        GameManager.OnGameUpdate -= EnemyUpdate;
        EventBus_EnemyManager.UnSubscribe(EnemyListUpdateHandler);
    }

    private void EnemyUpdate()
    {
        spawnManager.SpawnUpdate();

        for(int i = enemies.Count - 1; i >= 0; i--)
        {
            enemies[i].CustomUpdate();
        }
    }

    private void EnemyListUpdateHandler(EnemyUpdateEvent evt)
    {
        switch (evt.type)
        {
            case EnemyUpdateType.Regist:
                if(!enemies.Contains(evt.enemy))
                    enemies.Add(evt.enemy);
                break;
            case EnemyUpdateType.Unregist:
                if(enemies.Contains(evt.enemy))
                {
                    enemies.Remove(evt.enemy);
                    OnEnemyDeadEvent?.Invoke();
                }
                break;
        }
    }
}
