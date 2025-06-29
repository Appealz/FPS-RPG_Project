using UnityEngine;

public class EnemySuicideAttackCtrl : MonoBehaviour, IEnemyAttack
{
    private IEnemyContextReadable context;
    private IEnemyWeapon weapon;
    private IAnimHandle animCtrl;
    private bool isAttack;

    public void AttackUpdate()
    {
        if (!isAttack) return;

        OnAttack();
    }

    public void InitAttack(IEnemyWeapon newWeapon)
    {
        if (!TryGetComponent<IEnemyContextReadable>(out context))
            Debug.Log($"{gameObject.name} EnemyAttackSuidideAttackCtrl.cs - InitAttack() - Can't Reference EnemyContext");
        if (!TryGetComponent<IAnimHandle>(out animCtrl))
            Debug.Log($"{gameObject.name} EnemyAttackSuidideAttackCtrl.cs - InitAttack() - Can't Reference nimCtrl");
        else animCtrl.OnAttackEvent += OnAnimationEvent;
        weapon = newWeapon;
        isAttack = false;
    }

    public void OnAnimationEvent()
    {
        // todo 이벤트 타입들이 정해지면 그에 맞춰서 작동
    }

    public void OnAttack()
    {
        animCtrl.SetAnim("OnAttack");
    }

    public void SetEnable(bool isOn)
    {
        isAttack = isOn;
    }

    private void OnDisable()
    {
        animCtrl.OnAttackEvent -= OnAnimationEvent;
    }
}
