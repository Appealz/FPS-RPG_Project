using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public StatManager statManager { get; private set; }
    public PlayerInventory inventory { get; private set; }

    // todo 인게임 재화 클래스 구현

    /// <summary>
    /// todo 플레이어 스텟 데이터를 받아서 statManager에게 넘길 예정
    /// </summary>
    public void InitPlayerData()
    {
        // 추후 컨텍스트 매니저를 통해 스텟 데이터를 받아오면 그걸 기반으로 초기화
        statManager = new StatManager(new Dictionary<StatType, StatValue>
        {
            {StatType.HP, new StatValue(100) },
            {StatType.MoveSpeed, new StatValue(1) },
            {StatType.AttackDamage, new StatValue(0) }
        });
        if(!TryGetComponent<PlayerInventory>(out var inven))
        {
            Debug.Log("PlayerDataManager.cs - InitPlayerData() - PlayerInventory");
        }else inventory = inven;


    }
}
