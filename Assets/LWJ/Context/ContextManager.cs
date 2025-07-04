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
        // todo: ���� ����� ����� ������
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
// ��ȭ, �����۸���Ʈ, ����, ������ ������(Ŭ����(������ ����, ����, Ư��, ĳ������ ������ ��������������))
// + ���̵� ����
// ���ؽ�Ʈ�Ŵ���(�κ�)���� � ������ �����ߴ����� ���� �� ������ Ŭ������ �������� ����
// ���ӸŴ���(���Ӿ�)���� �ʿ��� Ŭ���� ����.