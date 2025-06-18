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
        // ���⼭ ������ ������Ʈ ó��
        // �ٵ� ����Ʈ�� �����ؾ��ϴ°� ��������Ʈ�� �����ϴ°� ���ؾ��ҵ�?
    }
}
