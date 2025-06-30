using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Revolver : MonoBehaviour, IRangeWeapon
{
    public bool useable => currentAmmo > 0 && !isAttacking;

    public AnimationClip useClip => null;

    public AnimationClip reloadClip => null;

    public AnimEventData reloadAnimData => throw new NotImplementedException();

    public AnimEventData useAnimData => throw new NotImplementedException();

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

    public void Use() => Attack();

    public void Attack()
    {
        if (!useable)
            return;
        currentAmmo--;
        isAttacking = true;
        FireDelay().Forget();
    }

    public void Reload()
    {
        if (currentMagazine == 0)
            // ���� ȣ��
            return;
        currentMagazine--;
        currentAmmo = myData.maxAmmo;
        // �ִϸ��̼� �̺�Ʈ���� ȣ��.
    }

    private async UniTaskVoid FireDelay()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(attackRate));
        isAttacking = false;
    }



    public void InitWeaponData(WeaponData newData)
    {
        myData = newData;
        currentAmmo = newData.maxAmmo;
        damage = newData.damagePerShot;
        attackRate = newData.fireRate;
    }

    public void StartReload()
    {
        isReloading = true;
    }

    public void CancelReload()
    {
        if(!isReloading) return;
        isReloading=false;
    }
}
