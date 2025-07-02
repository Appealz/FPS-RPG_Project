using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ShotGun : MonoBehaviour, IRangeWeapon, IDroppable
{
    public bool useable => currentAmmo > 0 && !isAttacking;

    public AnimationClip useClip => null;

    public AnimationClip reloadClip => null;

    public AnimEventData reloadAnimData => throw new NotImplementedException();

    public AnimEventData useAnimData => throw new NotImplementedException();

    public int itemID => myData.id;

    public float weaponRecoil => 1f;

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
        // 총알 미구현으로 임시 주석처리.
        //if(!useable)
        //    return;

        // 임시로 isAttacking으로 작동
        if (isAttacking)
            return;
        currentAmmo--;
        isAttacking = true;
        FireSpreadBullet();
        FireDelay().Forget();
    }

    private void FireSpreadBullet()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            // 원뿔형 퍼짐 방향 계산
            Vector3 spreadDir = GetSpreadDirection(spreadAngle);

            Transform camTrans = Camera.main.transform;
            // Raycast로 피격 판정
            if (Physics.Raycast(camTrans.position, spreadDir, out RaycastHit hit, 50f))
            {
                Debug.DrawRay(camTrans.position, spreadDir * hit.distance, Color.red, 1f);
                Debug.Log("조준한 대상: " + hit.collider.name);
            }
        }
    }

    private Vector3 GetSpreadDirection(float angle)
    {
        Transform camTrans = Camera.main.transform;
        float spreadRadius = MathF.Tan(angle * Mathf.Deg2Rad);

        Vector2 randomInCircle = UnityEngine.Random.insideUnitSphere * spreadRadius;

        Vector3 right = camTrans.right;
        Vector3 up = camTrans.up;

        Vector3 spreadDirection = (camTrans.forward + right * randomInCircle.x + up * randomInCircle.y).normalized;
        return spreadDirection;
    }

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
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
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

    public void Drop(Vector3 dropDir, float dropForce)
    {
        
    }
}

