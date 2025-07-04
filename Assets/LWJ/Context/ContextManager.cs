using System.Collections.Generic;
using UnityEngine;

public class ContextManager : DontDestroySingleton<ContextManager>
{    
    PlayGameContext playGameContext;
    EndGameContext endGameContext;

    int playLevel = 1;
    string playClassName = "rifler";

    Dictionary<itemSlotType, int> normalItemDictionary = new Dictionary<itemSlotType, int>();

    protected override void DoAwake()
    {
        base.DoAwake();
        normalItemDictionary[itemSlotType.Main] = 1001;
        normalItemDictionary[itemSlotType.Revolver] = 1016;
        normalItemDictionary[itemSlotType.Knife] = 1017;
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
        PlayGameContext testGameContext = new PlayGameContext(new ClassData(normalItemDictionary, 3), 3);
        return testGameContext;
    }
}

// PlyaerSaveData 
// 재화, 아이템리스트, 업적, 직업별 데이터(클래스(직업별 레벨, 스탯, 특전, 캐릭별로 마지막 장착아이템정보))
// + 난이도 정보
// 컨텍스트매니저(로비)에서 어떤 직업을 선택했는지에 따라서 그 직업의 클래스만 가져오는 형태
// 게임매니저(게임씬)에게 필요한 클래스 전달.