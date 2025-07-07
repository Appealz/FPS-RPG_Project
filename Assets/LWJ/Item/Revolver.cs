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

    public int itemID => myData.id;

    public float weaponRecoil => 0.5f;

    private bool isAttacking;
    private int currentAmmo;
    private int currentMagazine;

    private float damage;
    private float attackRate;
    private WeaponData_Entity myData;
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

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Debug.Log("������ ���: " + hit.collider.name);
        }
        
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



    public void InitWeaponData(WeaponData_Entity newData)
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

    public void InitData(ItemData newData)
    {
        if(newData is WeaponData weaponData)
        {
            myData = weaponData.data;
            currentAmmo = weaponData.maxAmmo;
            damage = weaponData.damagePerShot;
            attackRate = weaponData.fireRate;
        }
    }

    public CurrentData GetItemCurrentData()
    {
        throw new NotImplementedException();
    }

    public void Create(Pool onwerPool)
    {
        throw new NotImplementedException();
    }

    public void ReturnToPool()
    {
        throw new NotImplementedException();
    }
}
