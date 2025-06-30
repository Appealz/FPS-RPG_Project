using System;
using UnityEngine;

public class GameManager : DestroySingleton<GameManager>
{

    private bool isPause;

    #region _GameProcessDelegate_
    /// <summary>
    /// ������ ������Ʈ
    /// </summary>
    public static event Action OnGameUpdate;
    /// <summary>
    /// ������ ���� �� �� �۵��ؾ��� �ż���
    /// </summary>
    public static event Action OnGameEnd;
    /// <summary>
    /// ������ ����� �ؾ��� ��� �۵����Ѿ��� �ż���
    /// </summary>
    public static event Action OnGameClear;
    #endregion

    protected override void DoAwake()
    {
        
    }

    private void Start()
    {
        DontResetSetting();
        ResetSetting();
    }

    private void DontResetSetting()
    {
        EnemyAnimEventDataManager.InitEnemyAnimData();
    }

    private void ResetSetting()
    {
        isPause = false;
    }

    private void Update()
    {
        if (!isPause)
            OnGameUpdate?.Invoke();
    }

    private void PauseHandler(bool value)
    {
        isPause = value;
    }

    private void GameEndHandler()
    {
        OnGameEnd?.Invoke();
    }

    private void GameClearHandler()
    {
        OnGameClear?.Invoke();
    }
}
