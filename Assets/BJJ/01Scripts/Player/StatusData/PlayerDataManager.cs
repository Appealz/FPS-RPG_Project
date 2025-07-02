using System.Collections.Generic;
using UnityEngine;



public class PlayerDataManager : MonoBehaviour
{
    public StatManager statManager { get; private set; }
    public PlayerInventory inventory { get; private set; }

    public CurrencyManager currencyManager { get; private set; }

    /// <summary>
    /// todo 플레이어 스텟 데이터를 받아서 statManager에게 넘길 예정
    /// </summary>
    public void InitPlayerData()
    {
        // 추후 컨텍스트 매니저를 통해 스텟 데이터를 받아오면 그걸 기반으로 초기화
        statManager = new StatManager(gameObject,new Dictionary<StatType, StatValue>
        {
            {StatType.HP, new StatValue(100) },
            {StatType.MoveSpeed, new StatValue(1) },
            {StatType.AttackDamage, new StatValue(0) }
        });
        inventory = new PlayerInventory(gameObject, null);

        currencyManager = new CurrencyManager(gameObject);

        EventBus_Item.Subscribe(UpdateItemHandler);
        EventBus_Stat.Subscribe(UpdateStatHandler);
    }

    private void OnDisable()
    {
        EventBus_Item.UnSubscribe(UpdateItemHandler);
        EventBus_Stat.Unsubscribe(UpdateStatHandler);
    }

    // todo 이벤트 버스 구현 후 이벤트 버스로 이벤트를 받는 핸들러 구현 필요

    private void UpdateItemHandler(ItemChangedEvent newItemEvent)
    {
        if (newItemEvent.sender != gameObject) return;

        switch(newItemEvent.eventType)
        {
            case ItemEventType.add:
                inventory.AddItem(newItemEvent.changeItem);
                break;
            case ItemEventType.remove:
                inventory.RemoveItem(newItemEvent.changeItem);
                break;
        }
    }

    private void UpdateStatHandler(StatModifier modifier)
    {
        if (modifier.sender != gameObject) return;

        switch(modifier.EventType)
        {
            case StatEventType.Add:
                statManager.AddModifier(modifier.ChangedStatType, modifier.ChangeValue, modifier.isMulti);
                break;
            case StatEventType.Remove:
                statManager.RemoveModifier(modifier.ChangedStatType, modifier.ChangeValue, modifier.isMulti);
                break;
        }

    }

}
