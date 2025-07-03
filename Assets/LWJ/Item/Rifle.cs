using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rifle : MonoBehaviour, IRangeWeapon, IDroppable
{
    // ��������ü�� �ߵ��������� ����.
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

    // todo : ������ ������ ����
    // private ItemData itemData

    // �÷��̾� ���� ���� ����.
    public void SetStatus(float newDamage)
    {
        
    }

    public void Use() => Attack();

    public void Attack()
    {
        // �Ѿ� �̱������� �ӽ� �ּ�ó��.
        //if(!useable)
        //    return;

        // �ӽ÷� isAttacking���� �۵�
        if (isAttacking)
            return;

        currentAmmo--;
        isAttacking = true;
        Debug.Log($"rifle ����");

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Debug.Log("������ ���: " + hit.collider.name);
        }

        FireDelay().Forget();

        // todo : 1) �����½�ũ�� attackRate�� ���� isAttacking ����.
        //        2) �Ѿ� �� �����ɽ�Ʈ �߻�.
        
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
        // �ִϸ��̼� �̺�Ʈ���� ȣ��.
    }

    public void InitWeaponData(WeaponData_Entity newData)
    {
        myData = newData;
        currentAmmo = newData.maxAmmo;
        damage = newData.damagePerShot;
        attackRate = newData.fireRate;

        Debug.Log($"������ ���� ���� : {newData.name}, {newData.id}, {newData.fireRate}, {newData.ammoPerReload}, {newData.range}");
    }

    public void Drop(Vector3 dropDir, float dropForce)
    {
        transform.SetParent(null); // �տ��� �и�
        gameObject.SetActive(true);

        rb.isKinematic = false;
        rb.AddForce(dropDir * dropForce, ForceMode.Impulse);
    }





    // todo : currentAmmo źâ����ŭ ���� 






}
