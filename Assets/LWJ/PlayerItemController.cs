using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;

public class PlayerItemController : MonoBehaviour,IItemCtrl
{
    private IItem currentItem;
    private float itemUseRate;
    private bool isItemUseReady;
    // 사용 가능상태 여부
    private bool isUse;
    // 장전여부
    private bool isReload;

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
        isUse = false;
    }
    public void Equip(IItem newItem)
    {
        currentItem = newItem;
        EventBus_ItemClip.Publish(new ItemClipChangedEvent(newItem));
    }

    // 플레이어에서 상시 호출
    // Use상태 돌입시 실행.
    public void UseCurrentItem()
    {
        if (!isUse)
            return;
        if (!isUse || currentItem == null)
            return;
        if (!currentItem.useable)
            return;
        InputUse();
        //currentItem.Use();
        Debug.Log("아이템 사용");
    }

    // 플레이어에서 상시 호출
    // Reload상태 돌입시 실행.
    public void ReloadWeapon()
    {
        // 현재 착용 아이템이 IWeapon일 경우만 작동.
        if(currentItem is IRangeWeapon rangeWeapon)
        {            
            EventBus_ItemAnim.Publish(new ItemAnimEvent(gameObject, ItemAnimType.Reload));
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

    public void SetReloadEnable(bool isOn)
    {
        isReload = isOn;    
    }

    // 코루틴으로 임시 구현
    // todo : 유니태스크 사용 예정
    //private IEnumerator ItemUseRateTime()
    //{
    //    yield return new WaitForSeconds(itemUseRate);
    //    isItemUseReady = true;
    //}

    public void Equip_Handle(ItemChangedEvent newEvent)
    {
        if (newEvent.eventType != ItemEventType.equip || newEvent.sender != gameObject)
            return;
        Equip(newEvent.changeItem);
    }

    public void InputUse()
    {
        if(currentItem == null) return;

        if(currentItem is IWeapon weapon)
        {
            currentItem.Use();
        }
        else
        {
            EventBus_ItemAnim.Publish(new ItemAnimEvent(gameObject, ItemAnimType.Use));
        }
    }


}
