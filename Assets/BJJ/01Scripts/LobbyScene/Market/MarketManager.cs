using System.Collections.Generic;
using UnityEngine;

public class MarketUnlockData
{
    public int unlockIndex;

    public MarketUnlockData(int unlockIndex)
    {
        this.unlockIndex = unlockIndex;
    }
}

public class MarketManager
{
    //todo 플레이어의 계정 정보에서 무기 해금 여부를 확인합니다.
    
    public MarketManager()
    {
        // todo 플레이어의 계정 데이터에서 무기 해금 데이터를 가져옵니다.
        // 마켓에서 사용해야하는 이벤트 버스를 등록합니다.
    }

    public void DisableMarket()
    {
        // 이벤트 버스를 해제합니다.
    }

    private void UnlockWeapon(MarketUnlockData evt)
    {
        // todo 해금 여부를 확인
        // todo 해금를 해야하는 아이템이라면 계정의 돈이 있는지 확인합니다
        // todo 돈이 있다면, 해금을 합니다
    }
}
