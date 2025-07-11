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

    private bool isShoppingTime = false;
    private float shoppingTime = 180f;
    private float curTime = 0f;

    private RoundManager roundManager;
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
        roundManager = new RoundManager();
        roundManager.InitRoundManager();
        roundManager.OnRoundEnd += RoundEndHandler;
    }

    private void ResetSetting()
    {
        // Player Setting
        isPause = false;

        Player[] players = FindObjectsByType<Player>(FindObjectsSortMode.None);
        foreach (Player p in players)
        {
            PlayerScanManager.Instance.RegisterTarget(p);
        }

        EnemyTotalManager.Instance.InitEnemyManager();

        roundManager.StartRound();
    }

    private void Update()
    {
        if (!isPause)
        {
            OnGameUpdate?.Invoke();
            ShoppingTimeChecker();
        }
    }

    private void ShoppingTimeChecker()
    {
        if (!isShoppingTime) return;

        curTime += Time.deltaTime;

        if(curTime >= shoppingTime)
        {
            roundManager.StartRound();
            isShoppingTime = false;
        }
    }

    private void RoundEndHandler()
    {
        if(roundManager.IsFinalRound)
        {
            GameClearHandler();
            return;
        }

        isShoppingTime = true;
        curTime = 0f;
        ShopManager.Instance.ShopUpdate();
        // EventBus_ShopUI.Publish(true); 
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

    private void OnDisable()
    {
        roundManager.OnRoundEnd -= RoundEndHandler;
        roundManager.DisableRoundManager();
        
    }
}
