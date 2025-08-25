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
    string playClassName = "rifler";

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
            setSaveData = CreateNewSaveData(DataManager.Instance.GetBaseClassList());
        }
        else
        {
            setSaveData = newData;
        }
    }

    private PlayerSaveData CreateNewSaveData(List<BaseClassData> newClassDatas)
    {
        PlayerSaveData newData = new PlayerSaveData();

        foreach(var classData in newClassDatas)
        {
            var newClass = new ClassData();
            newClass.InitData(classData);
            newData.classDatas[classData.name] = newClass;
        }
        return newData;
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