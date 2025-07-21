using Cysharp.Threading.Tasks;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;

public class SentryGunWeapon : MonoBehaviour, ISentryGunWeapon
{
    private Transform mount;
    private Transform head;
    private Transform attackPoint;

    private bool isAttack;
    private bool canFire;
    private ISentryGunReadableContext context;

    private float rotateSpeed = 10f;
    
    public void InitWeapon(Transform mount)
    {
        this.mount = mount;
        head = MyUtility.GetChildrenTrans(mount, "head");
        if (head == null)
            Debug.Log("SentryGunWeapon.cs - InitWeapon() - Can't FInd Head");

        attackPoint = MyUtility.GetChildrenTrans(mount, "AttackPoint");
        if(attackPoint == null)
            Debug.Log("SentryGunWeapon.cs - InitWeapon() - Can't FInd AttackPoint");

        if (!TryGetComponent<ISentryGunReadableContext>(out context))
            Debug.Log("SentryGunWeapon.cs - InitWeapon() - Can't FInd context");

        isAttack = false;
        canFire = false;
    }

    public void OffAttack()
    {
        isAttack = false;
    }

    public void OnAttack()
    {
        isAttack = true;
        canFire = true;
    }

    public void WeaponUpdate()
    {
        if (!isAttack) return;

        RotateTarget(); // 공격 상태에 들어가면 타겟을 향해 조준해야함

        if (!canFire) return;
        Fire();
    }

    private void RotateTarget()
    {
        var dir = context.target.transform.position - transform.position;

        // mount
        Vector3 flatDir = new Vector3(dir.x, 0f, dir.z);
        Quaternion targetYaw = Quaternion.LookRotation(flatDir);
        mount.rotation = Quaternion.RotateTowards(mount.rotation, targetYaw, rotateSpeed * Time.deltaTime);
        // head
        Vector3 localDir = head.InverseTransformDirection(dir.normalized);
        float angleX = -Mathf.Atan2(localDir.y, localDir.z) * Mathf.Rad2Deg;
        head.localRotation = Quaternion.Euler(angleX, 0f, 0f);
    }

    private async void Fire()
    {
        canFire = false;
        var dir = context.target.transform.position - attackPoint.position;

        if(Physics.Raycast(attackPoint.position, dir, out var hit,context.GetStat(StatType.AttackRange), LayerMask.GetMask("Enemy")))
        {
            EventBus_Damage.Publish(new DamageInfo(context.owner, hit.collider.gameObject,
                                    context.GetStat(StatType.AttackDamage), null, DamageType.Damage));
        }
        
        await UniTask.WaitForSeconds(context.GetStat(StatType.AttackSpeed));
        canFire = true;
    }
}
