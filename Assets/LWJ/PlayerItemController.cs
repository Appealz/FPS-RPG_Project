using UnityEngine;

public class PlayerItemController : MonoBehaviour,IItemCtrl
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
    public void Init()
    {

    }
    public void Equip(IItem newItem)
    {
        currentItem = newItem;
    }

    public void UseCurrentItem()
    {
        currentItem.Use();
    }

    public void ReloadWeapon()
    {
        // ���� ���� �������� IWeapon�� ��츸 �۵�.
        if(currentItem is IRangeWeapon rangeWeapon)
        {
            rangeWeapon.Reload();
        }
    }

    public void Drop()
    {
        // ����Ʈ���� ���� ������ ������
        // ���� �ε����� �������� �������ų�
        // ���� �ε����� �������� ���°��
        // ���� �ε����� ������ ����
        // ����, Į�� ������ x
        
    }


}
