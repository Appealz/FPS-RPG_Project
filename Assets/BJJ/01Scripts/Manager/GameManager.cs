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
        PoolManager.Instance.InitPoolManager();
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
