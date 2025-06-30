using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Rifle : MonoBehaviour, IRangeWeapon
{
    // 아이템자체가 발동가능한지 여부.
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

    // todo : 아이템 데이터 주입
    // private ItemData itemData
    public void InitData()
    {
        
    }

    // 플레이어 관련 스텟 주입.
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

        // todo : 1) 유니태스크로 attackRate에 따라 isAttacking 관리.
        //        2) 총알 및 레이케스트 발사.
        
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
        // 애니메이션 이벤트에서 호출.
    }

    public void InitWeaponData(WeaponData newData)
    {
        myData = newData;
        currentAmmo = newData.maxAmmo;
        damage = newData.damagePerShot;
        attackRate = newData.fireRate;
    }





    // todo : currentAmmo 탄창수만큼 리셋 






}
