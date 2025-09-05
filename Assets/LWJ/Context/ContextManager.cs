using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}

public class ContextManager : DontDestroySingleton<ContextManager>
{    
    PlayGameContext playGameContext;
    EndGameContext endGameContext;

    int playLevel = 1;
    public string playClassName = "rifler";

    Dictionary<itemSlotType, int> normalItemDictionary = new Dictionary<itemSlotType, int>();
    List<int> normalItemList = new List<int>();

    PlayerSaveData setSaveData;
    Difficulty setDiff;

    protected override void DoAwake()
    {
        base.DoAwake();
        normalItemDictionary[itemSlotType.Main] = 1001;
        normalItemDictionary[itemSlotType.Sub] = 1006;
        normalItemDictionary[itemSlotType.Revolver] = 1016;
        normalItemDictionary[itemSlotType.Knife] = 1017;

        normalItemList.Add(1001);
        normalItemList.Add(1006);
        normalItemList.Add(1016);
        normalItemList.Add(1017);
    }

    public void InitPlayGameContext()
    {        
        playGameContext = new PlayGameContext(new ClassData(), 3);
    }

    public void SetSaveData(PlayerSaveData newData)
    {
        if(newData == null)
        {
            Debug.Log("세이브 데이터 오류");
        }
        else
        {
            setSaveData = newData;
        }
    }
        
    public void SetSelectClass(string newClassName)
    {
        playClassName = newClassName;
    }

    public ClassData GetSelectedClassData()
    {
        return setSaveData.classDatas[playClassName];
    }

    public void StartGameSetUp(PlayerSaveData newData)
    {
        playGameContext = new PlayGameContext(newData.classDatas[playClassName], playLevel);
    }
    

    public void EndGameDataSetUp()
    {
        // todo: 게임 종료시 저장될 데이터
    }

    public PlayGameContext GetPlayGameContext()
    {
        return playGameContext;
    }

    public PlayGameContext TestPlayGameContext()
    {
        PlayGameContext testGameContext = new PlayGameContext(new ClassData(), 3);
        return testGameContext;
    }
}

// PlyaerSaveData 
// 재화, 아이템리스트, 업적, 직업별 데이터(클래스(직업별 레벨, 스탯, 특전, 캐릭별로 마지막 장착아이템정보))
// + 난이도 정보
// 컨텍스트매니저(로비)에서 어떤 직업을 선택했는지에 따라서 그 직업의 클래스만 가져오는 형태
// 게임매니저(게임씬)에게 필요한 클래스 전달.

public class SelectionForm
{
    public string className;
    public Difficulty difficulty;
    public string playMap;

    public void SetClass(string newClass)
    {
        className = newClass;
    }

    public void SetDifficulty(Difficulty newDifficulty)
    {
        difficulty = newDifficulty;
    }

    public void SetMap(string newMap)
    {
        playMap = newMap;
    }
}