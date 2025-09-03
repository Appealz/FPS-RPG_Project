using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketUnlockEvent
{
    public int unlockIndex;

    public MarketUnlockEvent(int unlockIndex)
    {
        this.unlockIndex = unlockIndex;
    }
}

public class MarketManager
{
    private Dictionary<int, bool> unlockDatas;
    
    public MarketManager()
    {
        // todo 플레이어의 계정 데이터에서 무기 해금 데이터를 가져옵니다.
        unlockDatas = SaveLoadSystem.AccountData.unlockedItems;
    }

    private void UnlockWeapon(MarketUnlockEvent evt)
    {
        // todo 해금 여부를 확인
        // todo 해금를 해야하는 아이템이라면 계정의 돈이 있는지 확인합니다
        // todo 돈이 있다면, 해금을 합니다

        SaveLoadSystem.CheckDirty();
    }

    /// <summary>
    /// 뷰 모델쪽에서 이 메서드를 통해 데이터를 가져갈 예정
    /// 딕셔너리의 int / bool 형식을 그대로 가져가서 페어형태로 데이터를 보관
    /// 리스트를 읽어가면서 해당 데이터 내의 id값으로 이벤트를 던질 예정
    /// </summary>
    /// <returns></returns>
    public IReadOnlyList<KeyValuePair<int, bool>> GetUnlockDatas()
    {
        List<KeyValuePair<int, bool>> list = unlockDatas.ToList();
        return list;
    }
}
