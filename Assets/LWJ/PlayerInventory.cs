using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    private GameObject owner;
    private List<IItem> items = new List<IItem>();
    private IItem currentItem;
    private int currentIndex;

    public PlayerInventory(GameObject newOwner, List<IItem> newItemList)
    {
        owner = newOwner;
        items = newItemList;
    }

    // 1,2,3,4,5 Ű�� ���ε�
    public void EquipItem(int index)
    {        
        //currentIndex = index;
        //currentItem = items[currentIndex];
        Debug.Log($"{index}������ ����");
        // �̺�Ʈ�� PlayerItemController�� Equip(items[index]) ȣ��;
    }

    public void AddItem(IItem newItem)
    {
        items.Add(newItem);
    }

    public void RemoveItem(IItem removeItem)
    {
        if(items.Contains(removeItem))
        {
            items.Remove(removeItem);
        }
        else
        {
            Debug.Log($"{removeItem} �������� �κ��丮�� ����");
        }
        // ����Ʈ���� ���� ������ ������
        // ���� �ε����� �������� �������ų�
        // ���� �ε����� �������� ���°��
        // ���� �ε����� ������ ����
        // ����, Į�� ������ x
    }
}
