using System;
using UnityEngine;

public class GameManager : DestroySingleton<GameManager>
{

    private bool isPause;

    #region _GameProcessDelegate_
    /// <summary>
    /// 게임의 업데이트
    /// </summary>
    public static event Action OnGameUpdate;
    /// <summary>
    /// 게임이 종료 될 때 작동해야할 매서드
    /// </summary>
    public static event Action OnGameEnd;
    /// <summary>
    /// 게임을 재시작 해야할 경우 작동시켜야할 매서드
    /// </summary>
    public static event Action OnGameClear;
    #endregion

    protected override void DoAwake()
    {
        // 게임 매니저가 시작하면서 찾아야하는 것
    }

    private void Start()
    {
        DontResetSetting();
        ResetSetting();
    }

    private void DontResetSetting()
    {

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
