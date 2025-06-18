using UnityEngine;

[RequireComponent(typeof(EnemySpawnManager))]
public class EnemyTotalManager : DestroySingleton<EnemyTotalManager>
{
    private EnemySpawnManager spawnManager;

    protected override void DoAwake()
    {
        if (!TryGetComponent(out spawnManager))
            Debug.Log("EnemyTotalManager.cs - DoAwake() - SpawnManager Can't Find");
    }

    public void InitEnemyManager()
    {
        spawnManager.InitSpawnManager();

        GameManager.OnGameUpdate += EnemyUpdate;
    }

    private void OnDisable()
    {
        GameManager.OnGameUpdate -= EnemyUpdate;
    }

    private void EnemyUpdate()
    {
        // 여기서 적들의 업데이트 처리
        // 근데 리스트로 관리해야하는가 델리게이트로 관리하는가 정해야할듯?
    }
}
