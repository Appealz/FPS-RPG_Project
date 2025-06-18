using UnityEngine;

public class PlayerItemController : MonoBehaviour
{
    IItem currentItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Equip(IItem newItem)
    {
        currentItem = newItem;
    }

    public void UseCurrentItem()
    {

    }

    public void ReloadWeapon()
    {
        // 현재 착용 아이템이 IWeapon일 경우만 작동.
    }

    public void Drop()
    {
        // 리스트에서 현재 아이템 버리고
        // 다음 인덱스의 아이템을 꺼내오거나
        // 다음 인덱스의 아이템이 없는경우
        // 이전 인덱스의 아이템 착용
        // 권총, 칼은 버리기 x
        
    }
}
