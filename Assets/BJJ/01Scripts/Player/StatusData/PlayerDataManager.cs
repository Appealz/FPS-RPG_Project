using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public StatManager statManager { get; private set; }
    public PlayerInventory inventory { get; private set; }

    // todo �ΰ��� ��ȭ Ŭ���� ����

    /// <summary>
    /// todo �÷��̾� ���� �����͸� �޾Ƽ� statManager���� �ѱ� ����
    /// </summary>
    public void InitPlayerData()
    {
        // ���� ���ؽ�Ʈ �Ŵ����� ���� ���� �����͸� �޾ƿ��� �װ� ������� �ʱ�ȭ
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
