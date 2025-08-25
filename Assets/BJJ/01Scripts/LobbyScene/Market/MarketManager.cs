using System.Collections.Generic;
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
    //todo 플레이어의 계정 정보에서 무기 해금 여부를 확인합니다.
    PlayerSaveData playerData;
    
    public MarketManager()
    {
        // todo 플레이어의 계정 데이터에서 무기 해금 데이터를 가져옵니다.
    }

    private void UnlockWeapon(MarketUnlockEvent evt)
    {
        // todo 해금 여부를 확인
        // todo 해금를 해야하는 아이템이라면 계정의 돈이 있는지 확인합니다
        // todo 돈이 있다면, 해금을 합니다
    }
}
