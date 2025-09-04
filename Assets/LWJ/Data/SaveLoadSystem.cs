using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

public static class SaveLoadSystem
{
    private static PlayerSaveData accountData;
    public static PlayerSaveData AccountData
    {
        get
        {
            if (accountData == null)
            {
                LoadData();
            }
            return accountData;
        }
    }

    private static bool isInit = false;
    public static bool IsInit => isInit;

    private static string dataPath = "PlayerSaveData.json";

    private static bool isDirty = false;
    public static void CheckDirty()
    {
        isDirty = true;
    }

    public static PlayerSaveData Load()
    {
        Debug.Log($"[SaveLoadSystem] Load() 실행, isInit={isInit}");
        if (isInit)            
            return accountData;
        

        string path = Path.Combine(Application.persistentDataPath, dataPath);

        if(File.Exists(path))
        {
            Debug.Log("데이터 존재 로드합니다.");
            string data = File.ReadAllText(path);
            accountData = JsonConvert.DeserializeObject<PlayerSaveData>(data);
        }
        else
        {
            Debug.Log("데이터 없음 신규 데이터 생성");
            NewAccountData();
        }
        isInit = true;

        //if (accountData.classDatas == null)
        //{
        //    Debug.LogError("[Check] classDatas == null");
        //}
        //else
        //{
        //    Debug.Log($"[Check] classDatas Count = {accountData.classDatas.Count}");
        //}

        //foreach (var kvp in accountData.classDatas)
        //{
        //    Debug.Log($"[Check] classDatas key = '{kvp.Key}'");
        //}

        return accountData;
    }

    public static void LoadData()
    {
        if (isInit) return;

        string path = Path.Combine(Application.persistentDataPath, dataPath);

        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            accountData = JsonConvert.DeserializeObject<PlayerSaveData>(data);
        }
        else
        {
            NewAccountData();
        }

        SaveLoop().Forget();
        isInit = true;
    }
    
    /// <summary>
    /// 기본적으로 데이터 로드를 시도하고 데이터가 없을 시 새데이터 생성 후 저장을 하지만
    /// 종종 플레이어가 새로운 상태로 시작하고 싶을때를 위해 public으로 열어둠
    /// </summary>
    public static void NewAccountData()
    {
        List<BaseClassData> baseClassDatas = DataManager.Instance.GetBaseClassList();

        accountData = new PlayerSaveData
        {
            currency = 0,
            unlockedItems = new Dictionary<int, bool>(),
            achievementData = new AchievementStat
            {
                enemyKill = 0,
                clearCount = 0,
                healAmount = 0,
                achievementData = new Dictionary<int, AchivementProgress>(),
            },
            classDatas = new Dictionary<string, ClassData>()
        };

        foreach (var classData in baseClassDatas)
        {
            Debug.Log($"[SaveData] 등록된 클래스: '{classData.name}'");

            var newClass = new ClassData();
            newClass.InitData(classData);

            accountData.classDatas[classData.name] = newClass;
        }
        SaveData();
    }

    public static void AddPlayData(EndGameContext data)
    {
        // todo 컨텍스트 쪽에서 게임 종료시 해당 데이터를 바로 넣는다
        // 업적은 업적 매니저가 직접 탐지하겠지만 계정 재화같은 부분들은 여기서 넣든가하면 될거같네요
    }

    public static void Save(PlayerSaveData saveData)
    {
        string path = Path.Combine(Application.persistentDataPath, dataPath);
        string data = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        SaveDelay(path, data);
    }
    
    public static void SaveData()
    {
        string path = Path.Combine(Application.persistentDataPath, dataPath);
        string data = JsonConvert.SerializeObject(accountData, Formatting.Indented);
        SaveDelay(path, data);
    }

    /// <summary>
    /// 런타임 중에 데이터를 세이브하려 하다보면 프레임 드랍이 생길 여지가 있어서 어느정도 비동기로 처리함
    /// </summary>
    /// <param name="path"></param>
    /// <param name="data"></param>
    private async static void SaveDelay(string path, string data)
    {
        await Task.Run(() =>
        {
            File.WriteAllText(path, data);
        });
    }

    private static async UniTaskVoid SaveLoop()
    {
        while(true)
        {
            await UniTask.WaitForSeconds(5f);

            if(isDirty)
            {
                SaveData();
                isDirty = false;
            }
        }
    }
}