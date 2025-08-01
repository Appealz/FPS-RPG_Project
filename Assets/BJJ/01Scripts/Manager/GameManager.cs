using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
        DontResetSetting().Forget();
        ResetSetting();
    }

    private async UniTaskVoid DontResetSetting()
    {
        EnemyAnimEventDataManager.InitEnemyAnimData();
        roundManager = new RoundManager();
        roundManager.InitRoundManager();
        roundManager.OnRoundEnd += RoundEndHandler;
        ShopManager.Instance.InitShop();
        TestUIManager.Instance.InitTestUI();
        await SetStaticObject();
    }

    private async UniTask SetStaticObject()
    {
        var groups = await Addressables.LoadResourceLocationsAsync("Effect").ToUniTask();

        if (groups == null || groups.Count == 0)
        {
            Debug.Log("GameManager.cs - SetStaticObject() - Effect Label Non");
            return;
        }

        foreach (var efx in groups)
        {
            string path = efx.PrimaryKey;
            PoolManager.Instance.PoolRegist(path);
        }

        var groups2 = await Addressables.LoadResourceLocationsAsync("SkillObject").ToUniTask();

        if (groups2 == null || groups2.Count == 0)
        {
            Debug.Log("GameManager.cs - SetStaticObject() - SkillObject Label Non");
            return;
        }

        foreach (var obj in groups2)
        {
            string path = obj.PrimaryKey;
            PoolManager.Instance.PoolRegist(path);
        }
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
            GameEndHandler();
            return;
        }

        isShoppingTime = true;
        curTime = 0f;
        EventBus_ShopIsOn.Publish(new ShopIsOnEvent(true));
        ShopManager.Instance.ShopUpdate();
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
