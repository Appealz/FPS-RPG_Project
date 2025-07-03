using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerItemController : MonoBehaviour,IItemCtrl
{
    //[SerializeField]
    //private Rifle testWeapon;
    [SerializeField]
    private ShotGun testWeapon;
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
        weaponHolder.AttachWeapon(newItem.itemID);
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
            rangeWeapon.CancelReload();
        }
    }

    // ������ Ű�� ���ε�
    public void Drop()
    {
        //Debug.Log("������ ���");
        if (currentItem == null)
            return;

        if (currentItem is IDroppable dropItem)
        {
            Vector3 forwardDir = transform.forward + Vector3.up * 0.3f;
            dropItem.Drop(forwardDir.normalized, 5f);
            EventBus_Item.Publish(new ItemChangedEvent(currentItem, gameObject, ItemEventType.remove, currentItem.itemID));
        }
        else
            return;

        
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

        if(currentItem is IRangeWeapon weapon)
        {
            currentItem.Use();
            // todo : 0.1f�� �ӽ�, �� ������ �ݵ� ���� ����
            weaponHolder.WeaponRecoil(weapon.weaponRecoil);
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
