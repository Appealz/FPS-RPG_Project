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
        // ���� ���� �������� IWeapon�� ��츸 �۵�.
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
