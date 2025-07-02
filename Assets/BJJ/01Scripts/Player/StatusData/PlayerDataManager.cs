using System.Collections.Generic;
using UnityEngine;



public class PlayerDataManager : MonoBehaviour
{
    public StatManager statManager { get; private set; }
    public PlayerInventory inventory { get; private set; }

    public CurrencyManager currencyManager { get; private set; }

    /// <summary>
    /// todo �÷��̾� ���� �����͸� �޾Ƽ� statManager���� �ѱ� ����
    /// </summary>
    public void InitPlayerData()
    {
        // ���� ���ؽ�Ʈ �Ŵ����� ���� ���� �����͸� �޾ƿ��� �װ� ������� �ʱ�ȭ
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

    // todo �̺�Ʈ ���� ���� �� �̺�Ʈ ������ �̺�Ʈ�� �޴� �ڵ鷯 ���� �ʿ�

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
