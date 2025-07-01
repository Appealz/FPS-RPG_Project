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
    // ��� ���ɻ��� ����
    private bool isUse;
    // ��������
    private bool isReload;

    private IPlayerAnimHandle animHandle;

    // ���� �ݵ�, �ӽ� ����
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

    // �÷��̾�� ��� ȣ��
    // Use���� ���Խ� ����.
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
        //Debug.Log("������ ���");
    }

    // �÷��̾�� ��� ȣ��
    // Reload���� ���Խ� ����.
    public void ReloadWeapon()
    {
        Debug.Log("���� ����");
        // ���� ���� �������� IWeapon�� ��츸 �۵�.
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

    // ������ Ű�� ���ε�
    public void Drop()
    {
        // ��� Ű �߻�
        // 
    }

    public void SetEnable(bool isOn)
    {
        isUse = isOn;
        Debug.Log($"isUse ����: {isUse}");
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
            // todo : 0.1f�� �ӽ�, �� ������ �ݵ� ���� ����
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
