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

        EventBus_Item.Subscribe(AddItemHandler);
        EventBus_Item.Subscribe(RemoveItemHandler);
        EventBus_Stat.Subscribe(AddStatHandler);
        EventBus_Stat.Subscribe(RemoveStatHandler);
    }

    private void OnDisable()
    {
        EventBus_Item.UnSubscribe(AddItemHandler);
        EventBus_Item.UnSubscribe(RemoveItemHandler);
        EventBus_Stat.Unsubscribe(AddStatHandler);
        EventBus_Stat.Unsubscribe(RemoveStatHandler);
    }

    // todo 이벤트 버스 구현 후 이벤트 버스로 이벤트를 받는 핸들러 구현 필요

    private void AddItemHandler(ItemChangedEvent newItemEvent)
    {
        if (newItemEvent.sender != gameObject || newItemEvent.eventType != ItemEventType.add) return;

        inventory.AddItem(newItemEvent.changeItem);
    }

    private void RemoveItemHandler(ItemChangedEvent newItemEvent)
    {
        if (newItemEvent.sender != gameObject || newItemEvent.eventType != ItemEventType.add) return;

        inventory.RemoveItem(newItemEvent.changeItem);
    }

    private void AddStatHandler(StatModifier modifier)
    {
        if (modifier.sender != gameObject || modifier.EventType != StatEventType.Add) return;

        statManager.AddModifier(modifier.ChangedStatType, modifier.ChangeValue, modifier.isMulti);
    }

    private void RemoveStatHandler(StatModifier modifier)
    {
        if (modifier.sender != gameObject || modifier.EventType != StatEventType.Remove) return;

        statManager.RemoveModifier(modifier.ChangedStatType, modifier.ChangeValue, modifier.isMulti);
    }
}
