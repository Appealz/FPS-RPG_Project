using System.Collections;
using UnityEngine;

public class PlayerItemController : MonoBehaviour,IItemCtrl
{
    private IItem currentItem;
    private float itemUseRate;
    private bool isItemUseReady;
    private bool isUse;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init()
    {
        isItemUseReady = true;
        isUse = true;
    }
    public void Equip(IItem newItem)
    {
        currentItem = newItem;
    }

    public void UseCurrentItem()
    {
        if (!isUse || !isItemUseReady || currentItem == null)
            return;
        currentItem.Use();
    }

    public void ReloadWeapon()
    {
        // 현재 착용 아이템이 IWeapon일 경우만 작동.
        if(currentItem is IRangeWeapon rangeWeapon)
        {
            rangeWeapon.Reload();
        }
    }

    public void Drop()
    {
        // 리스트에서 현재 아이템 버리고
        // 다음 인덱스의 아이템을 꺼내오거나
        // 다음 인덱스의 아이템이 없는경우
        // 이전 인덱스의 아이템 착용
        // 권총, 칼은 버리기 x
    }

    public void SetEnable(bool isOn)
    {
        isUse = isOn;
    }

    // 코루틴으로 임시 구현
    // todo : 유니태스크 사용 예정
    private IEnumerator ItemUseRateTime()
    {
        yield return new WaitForSeconds(itemUseRate);
        isItemUseReady = true;
    }
}
