using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Rifle : MonoBehaviour, IRangeWeapon
{
    // ��������ü�� �ߵ��������� ����.
    public bool useable => currentAmmo > 0 && !isAttacking;

    public AnimationClip useClip => null;
    public AnimationClip reloadClip => null;

    public AnimEventData reloadAnimData => null;

    public AnimEventData useAnimData => null;

    private bool isAttacking;
    private int currentAmmo;
    private int currentMagazine;

    private float damage;    
    private float attackRate;
    private WeaponData myData;
    private bool isReloading;

    // todo : ������ ������ ����
    // private ItemData itemData
    public void InitData()
    {
        
    }

    // �÷��̾� ���� ���� ����.
    public void SetStatus(float newDamage)
    {
        
    }

    public void Use() => Attack();

    public void Attack()
    {        
        if(!useable)
            return;
        currentAmmo--;
        isAttacking = true;
        FireDelay().Forget();

        // todo : 1) �����½�ũ�� attackRate�� ���� isAttacking ����.
        //        2) �Ѿ� �� �����ɽ�Ʈ �߻�.
        
    }

    private async UniTaskVoid FireDelay()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(attackRate));
        isAttacking = false;
    }
    public void StartReload()
    {
        isReloading = true;
    }
    public void CancelReload()
    {
        if (!isReloading)
            return;
        isReloading = false;
    }
    public void Reload()
    {        
        if (currentMagazine == 0 || isReloading)
            return;
        currentMagazine--;
        currentAmmo = myData.maxAmmo;
        isReloading = false;
        // �ִϸ��̼� �̺�Ʈ���� ȣ��.
    }

    public void InitWeaponData(WeaponData newData)
    {
        myData = newData;
        currentAmmo = newData.maxAmmo;
        damage = newData.damagePerShot;
        attackRate = newData.fireRate;
    }





    // todo : currentAmmo źâ����ŭ ���� 






}
