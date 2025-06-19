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

    // ���콺�� ���ε�
    public void UseCurrentItem()
    {
        if (!isUse || !isItemUseReady || currentItem == null)
            return;
        currentItem.Use();
    }

    // ���� Ű�� ���ε�
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

    // �ڷ�ƾ���� �ӽ� ����
    // todo : �����½�ũ ��� ����
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
