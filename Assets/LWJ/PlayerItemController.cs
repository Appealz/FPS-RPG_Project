using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;

public class PlayerItemController : MonoBehaviour,IItemCtrl
{
    private IItem currentItem;
    private float itemUseRate;
    private bool isItemUseReady;
    // ��� ���ɻ��� ����
    private bool isUse;
    // ��������
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
    }

    // �÷��̾�� ��� ȣ��
    // Use���� ���Խ� ����.
    public void UseCurrentItem()
    {
        if (!isUse || currentItem == null)
            return;
        if (!currentItem.useable)
            return;
        currentItem.Use();
    }

    // �÷��̾�� ��� ȣ��
    // Reload���� ���Խ� ����.
    public void ReloadWeapon()
    {
        // ���� ���� �������� IWeapon�� ��츸 �۵�.
        if(currentItem is IRangeWeapon rangeWeapon)
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
    }

    public void SetReloadEnable(bool isOn)
    {
        isReload = isOn;    
    }


    // �ڷ�ƾ���� �ӽ� ����
    // todo : �����½�ũ ��� ����
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
            UseCurrentItem();
        }
        else
        {

        }
    }


}
