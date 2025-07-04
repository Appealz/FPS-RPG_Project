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
            {StatType.AttackDamage, new StatValue(0) },
            {StatType.AttackSpeed, new StatValue(1f) }
        });
        inventory = new PlayerInventory(gameObject, ContextManager.Instance.TestPlayGameContext().playClassData.equippedItemDictionary);

        currencyManager = new CurrencyManager(gameObject);

        EventBus_Item.Subscribe(UpdateItemHandler);
        EventBus_Stat.Subscribe(UpdateStatHandler);
        EventBus_Buff.Subscribe(UpdateBuffHandler);
        EventBus_Currency.Subscribe(CurrecyChangeHandler);
        EventBus_CurrencyCheck.Subscribe(CurrencyCheckHandler);
        EventBus_CurrencyQuery.Subscribe(CurrencyQueryHandler);
    }

    private void OnDisable()
    {
        EventBus_Item.UnSubscribe(UpdateItemHandler);
        EventBus_Stat.Unsubscribe(UpdateStatHandler);
        EventBus_Buff.UnSubscribe(UpdateBuffHandler);
        EventBus_Currency.UnSubscribe(CurrecyChangeHandler);
        EventBus_CurrencyCheck.UnSubscribe(CurrencyCheckHandler);
        EventBus_CurrencyQuery.UnSubscribe(CurrencyQueryHandler);
    }

    // todo 이벤트 버스 구현 후 이벤트 버스로 이벤트를 받는 핸들러 구현 필요

    private void UpdateItemHandler(ItemChangedEvent newItemEvent)
    {
        if (newItemEvent.sender != gameObject) return;

        switch(newItemEvent.eventType)
        {
            case ItemEventType.add:
                inventory.AddItem(newItemEvent.itemID);
                break;
            case ItemEventType.remove:
                inventory.RemoveItem(newItemEvent.itemID);
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

    private void UpdateBuffHandler(BuffEvent evt)
    {
        if (evt.receiver != gameObject) return;

        switch (evt.Type)
        {
            case BuffEventType.Add:
                statManager.AddBuff(evt.Buff);
                break;
            case BuffEventType.Remove:
                // todo RemoveBuff
                break;
        }
    }

    private void CurrecyChangeHandler(CurrencyChangeEvent evt)
    {
        if (evt.receiver != gameObject) return;

        switch(evt.eventType)
        {
            case CurrencyChangeEventType.Add:
                currencyManager.AddGold(evt.value);
                break;
            case CurrencyChangeEventType.Remove:
                currencyManager.RemoveGold(evt.value);
                break;
        }
    }

    private void CurrencyCheckHandler(CurrencyCheckEvent evt)
    {
        if (evt.receiver != gameObject) return;

        bool canAfford = currencyManager.GetGold() >= evt.price;
        evt.callback?.Invoke(canAfford);
    }

    private void CurrencyQueryHandler(CurrencyQueryEvent evt)
    {
        if(evt.receiver != gameObject) return;

        evt.onResult(currencyManager.GetGold());
    }
}
