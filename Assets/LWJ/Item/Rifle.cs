using UnityEngine;

public class Rifle : MonoBehaviour, IRangeWeapon
{
    // ��������ü�� �ߵ��������� ����.
    public bool useable => currentAmmo > 0 || !isAttacking;
    private bool isAttacking;
    private int currentAmmo;
    private int currentMagazine;

    // todo : ������ ������ ����
    // private ItemData itemData
    public void InitData()
    {
        
    }

    public void Use() => Fire();

    private void Fire()
    {        
        if(!useable)
            return;
        isAttacking = true;
        currentAmmo--;
        // todo : 1) �����½�ũ�� attackRate�� ���� isAttacking ����.
        //        2) �Ѿ� �� �����ɽ�Ʈ �߻�.
    }

    public void Reload()
    {        
        if (currentMagazine == 0)
            return;
        currentMagazine--;
        
        // �ִϸ��̼� �̺�Ʈ�� �̺�Ʈ������
    }

    // todo : currentAmmo źâ����ŭ ���� 






}
