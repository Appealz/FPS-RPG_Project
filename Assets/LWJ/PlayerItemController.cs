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
    private void OnEnable()
    {
        EventBus_Item.Subscribe(Equip_Handle);
    }

    private void OnDisable()
    {
        EventBus_Item.UnSubscribe(Equip_Handle);
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

    // 마우스와 바인딩
    public void UseCurrentItem()
    {
        if (!isUse || !isItemUseReady || currentItem == null)
            return;
        currentItem.Use();
    }

    // 장전 키와 바인딩
    public void ReloadWeapon()
    {
        // 현재 착용 아이템이 IWeapon일 경우만 작동.
        if(currentItem is IRangeWeapon rangeWeapon)
        {
            rangeWeapon.Reload();
        }
    }

    // 버리기 키와 바인딩
    public void Drop()
    {
        // 드랍 키 발생
        // 
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

    public void Equip_Handle(ItemChangedEvent newEvent)
    {
        if (newEvent.eventType != ItemEventType.equip || newEvent.sender != gameObject)
            return;
        Equip(newEvent.changeItem);
    }
}
