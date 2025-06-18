using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<IItem> items = new List<IItem>();
    private IItem currentItem;
    private int currentIndex;

    public void EquipItem(int index)
    {
        currentIndex = index;
        currentItem = items[currentIndex];
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
