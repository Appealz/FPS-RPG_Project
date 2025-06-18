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
            Debug.Log($"{removeItem} 아이템은 인벤토리에 없음");
        }
        // 리스트에서 현재 아이템 버리고
        // 다음 인덱스의 아이템을 꺼내오거나
        // 다음 인덱스의 아이템이 없는경우
        // 이전 인덱스의 아이템 착용
        // 권총, 칼은 버리기 x
    }
}
