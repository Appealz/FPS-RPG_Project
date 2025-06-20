using UnityEngine;

public class ContextManager : DontDestroySingleton<ContextManager>
{    
    PlayGameContext playGameContext;
    EndGameContext endGameContext;

    int playLevel = 1;
    string playClassName = "rifler";

    public void StartGameSetUp(PlayerSaveData newData)
    {
        playGameContext = new PlayGameContext(newData.classDatas[playClassName], playLevel);
    }

    public void EndGameDataSetUp()
    {
        // todo: ���� ����� ����� ������
    }
}

// PlyaerSaveData 
// ��ȭ, �����۸���Ʈ, ����, ������ ������(Ŭ����(������ ����, ����, Ư��, ĳ������ ������ ��������������))
// + ���̵� ����
// ���ؽ�Ʈ�Ŵ���(�κ�)���� � ������ �����ߴ����� ���� �� ������ Ŭ������ �������� ����
// ���ӸŴ���(���Ӿ�)���� �ʿ��� Ŭ���� ����.