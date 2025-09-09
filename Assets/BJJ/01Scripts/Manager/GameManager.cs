using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using System;
using System.Security.Cryptography;
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
        if (!SettingManager.Instance.IsInit)
            SettingManager.Instance.SettingInit();

        StartFlow().Forget();
    }

    private async UniTaskVoid StartFlow()
    {
        // 테스트 코드임
        await MapLoadingTest.Instance.StartAsync();
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
        SettingManager.Instance.PlayStart();
        UIManager.Instance.InitPlayUI();

        EventBus_Pause.Subscribe(PauseHandler);
        EventBus_ExitEvent.Subscribe(GameEndHandler);

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

        // 플레이어를 시작점에서 스폰시키는 코드로 리펙토링 필요?
        Player player = FindAnyObjectByType<Player>();
        if (player != null)
        {
            PlayerScanManager.Instance.RegisterTarget(player);
            player.Init();
        }

        GameObject startPos = GameObject.FindGameObjectWithTag("PlayerSpawn");
        if(startPos != null)
            player.transform.position = startPos.transform.position;

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

    /// <summary>
    /// 추후에 일시정지용 Event가 만들어지면 변경될 예정
    /// </summary>
    /// <param name="value">추후에 일시정지 이벤트가 생기면 해당 이벤트로 파라미터 변경필요</param>
    private void PauseHandler(PauseEvent evt)
    {
        isPause = evt.isOn;
        Time.timeScale = isPause == true ? 1f : 0f;
    }

    // 로비씬으로 넘어가는걸 로딩씬을 거쳐서 할지 그냥 바로 넘어갈지 고민을 해봐야할듯
    private void GameEndHandler()
    {
        OnGameEnd?.Invoke();

    }

    private void GameEndHandler(ExitEvent evt)
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
        EventBus_Pause.UnSubscribe(PauseHandler);
        EventBus_ExitEvent.UnSubscribe(GameEndHandler);
        //SettingManager.Instance.PlayEnd();
    }
}
