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
    }

    // todo �̺�Ʈ ���� ���� �� �̺�Ʈ ������ �̺�Ʈ�� �޴� �ڵ鷯 ���� �ʿ�
}
