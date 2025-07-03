using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rifle : MonoBehaviour, IRangeWeapon, IDroppable
{
    // 아이템자체가 발동가능한지 여부.
    public bool useable => currentAmmo > 0 && !isAttacking;

    public AnimationClip useClip => null;
    public AnimationClip reloadClip => null;

    public AnimEventData reloadAnimData => null;

    public AnimEventData useAnimData => null;

    public int itemID => myData.id;

    public float weaponRecoil => 0.1f;

    private Animator anims;

    private bool isAttacking;
    private int currentAmmo;
    private int currentMagazine;

    private float damage;    
    private float attackRate;
        
    private WeaponData_Entity myData;
    private bool isReloading;

    private Rigidbody rb;
    private void Awake()
    {
        if(!TryGetComponent<Animator>(out anims))
        {
            Debug.Log("Rifle - anim is not ref");
        }

        if(!TryGetComponent<Rigidbody>(out rb))
        {
            Debug.Log("Rifle - rb is not ref");
        }
    }

    // todo : 아이템 데이터 주입
    // private ItemData itemData

    // 플레이어 관련 스텟 주입.
    public void SetStatus(float newDamage)
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
        Debug.Log($"rifle 공격");

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Debug.Log("조준한 대상: " + hit.collider.name);
        }

        FireDelay().Forget();

        // todo : 1) 유니태스크로 attackRate에 따라 isAttacking 관리.
        //        2) 총알 및 레이케스트 발사.
        
    }

    private async UniTaskVoid FireDelay()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        isAttacking = false;
    }
    public void StartReload()
    {
        isReloading = true;
        anims.SetTrigger("Reload");
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

    public void InitWeaponData(WeaponData_Entity newData)
    {
        myData = newData;
        currentAmmo = newData.maxAmmo;
        damage = newData.damagePerShot;
        attackRate = newData.fireRate;

        Debug.Log($"데이터 주입 성공 : {newData.name}, {newData.id}, {newData.fireRate}, {newData.ammoPerReload}, {newData.range}");
    }

    public void Drop(Vector3 dropDir, float dropForce)
    {
        transform.SetParent(null); // 손에서 분리
        gameObject.SetActive(true);

        rb.isKinematic = false;
        rb.AddForce(dropDir * dropForce, ForceMode.Impulse);
    }





    // todo : currentAmmo 탄창수만큼 리셋 






}
