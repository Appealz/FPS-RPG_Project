using UnityEngine;

public class EnemyTotalManager : DestroySingleton<EnemyTotalManager>
{
    private EnemySpawnManager spawnManager;

    protected override void DoAwake()
    {
        if (!TryGetComponent<EnemySpawnManager>(out spawnManager))
            Debug.Log("EnemyTotalManager.cs - DoAwake() - SpawnManager Can't Find");
    }

    public void InitEnemyManager()
    {

    }
}
