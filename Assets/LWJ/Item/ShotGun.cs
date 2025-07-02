using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class ShotGun : MonoBehaviour, IRangeWeapon
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

    private float bulletCount = 10f;
    private float spreadAngle = 8f;


    // todo : 아이템 데이터 주입
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

    //private Vector3 GetSpreadDirection(Vector3 forward, float angle)
    //{
    //    Transform cam = Camera.main.transform;
    //    float spreadRadius = MathF.Tan(angle * Mathf.Deg2Rad);

    //    Vector2 randomInCircle = UnityEngine.Random.insideUnitSphere * spreadRadius;



    //    Vector3 spreadDirection;
    //    return spreadDirection;
    //}

    public void Reload()
    {
        if (currentMagazine == 0)
            // 사운드 호출
            return;
        currentMagazine--;
        currentAmmo = myData.maxAmmo;
        // 애니메이션 이벤트에서 호출.
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
        if (!isReloading) return;
        isReloading = false;
    }
}

