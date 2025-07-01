using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;

public class PlayerItemController : MonoBehaviour,IItemCtrl
{
    [SerializeField]
    private Rifle testWeapon;
    private IItem currentItem;
    private float itemUseRate;
    private bool isItemUseReady;
    // 사용 가능상태 여부
    private bool isUse;
    // 장전여부
    private bool isReload;

    private IPlayerAnimHandle animHandle;

    // 무기 반동, 임시 참조
    [SerializeField]
    private PlayerWeaponHolder weaponHolder;

    private void Awake()
    {
        if (!TryGetComponent<IPlayerAnimHandle>(out animHandle))
            Debug.Log("animHandle is not ref");
        if(testWeapon.TryGetComponent<IItem>(out currentItem))
        {
            Debug.Log($"{currentItem}");
        }

    }

    private void OnEnable()
    {
        EventBus_Item.Subscribe(Equip_Handle);
        animHandle.OnUseEvent += UseItem;
        animHandle.OnReloadEvent += Reload;
        animHandle.OnReloadCancel += CancelReload;
    }

    private void OnDisable()
    {
        EventBus_Item.UnSubscribe(Equip_Handle);
        animHandle.OnUseEvent -= UseItem;
        animHandle.OnReloadEvent -= Reload;
        animHandle.OnReloadCancel -= CancelReload;
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
        //if (!currentItem.useable)
        //    return;
        InputUse();
        //currentItem.Use();
        //Debug.Log("아이템 사용");
    }

    // 플레이어에서 상시 호출
    // Reload상태 돌입시 실행.
    public void ReloadWeapon()
    {
        Debug.Log("장전 시작");
        // 현재 착용 아이템이 IWeapon일 경우만 작동.
        if(currentItem is IRangeWeapon rangeWeapon)
        {            
            EventBus_ItemAnim.Publish(new ItemAnimEvent(gameObject, ItemAnimType.Reload, rangeWeapon.reloadAnimData));
            rangeWeapon.StartReload();
        }
    }

    public void Reload()
    {
        if (currentItem is IRangeWeapon rangeWeapon)
        {
            rangeWeapon.Reload();
        }
    }

    public void CancelReload()
    {
        if (currentItem is IRangeWeapon rangeWeapon)
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
        Debug.Log($"isUse 상태: {isUse}");
    }

    public void SetReloadEnable(bool isOn)
    {
        isReload = isOn;    
    }


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
            // todo : 0.1f는 임시, 각 무기의 반동 적용 예정
            weaponHolder.WeaponRecoil(0.1f);
        }
        else
        {            
            EventBus_ItemAnim.Publish(new ItemAnimEvent(gameObject, ItemAnimType.Use, currentItem.useAnimData));
        }
    }

    public void UseItem()
    {
        if (!isUse)
            return;
        if (!isUse || currentItem == null)
            return;
        if (!currentItem.useable)
            return;
        currentItem.Use();
    }

}
