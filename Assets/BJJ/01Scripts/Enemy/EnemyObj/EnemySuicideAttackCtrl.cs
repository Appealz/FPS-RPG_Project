using System;
using UnityEngine;

public interface ISuicideAttack
{
    event Action OnSuicideEvent;
}

public class EnemySuicideAttackCtrl : MonoBehaviour, IEnemyAttack, ISuicideAttack
{
    private IEnemyContextReadable context;
    private IEnemyWeapon weapon;
    private IAnimHandle animCtrl;
    private bool isAttack;

    public event Action OnSuicideEvent;

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
        else animCtrl.OnAnimFinishEvent += OnAnimationEvent;
        weapon = newWeapon;
        weapon.Init(gameObject);
        isAttack = false;
    }

    public void OnAnimationEvent()
    {
        weapon.OnAttack(context.attackRange, context.damage);
        OnSuicideEvent?.Invoke();
    }

    public void OnAttack()
    {
        isAttack = false;
        animCtrl.SetAnim("OnAttack");
    }

    public void SetEnable(bool isOn)
    {
        isAttack = isOn;
    }

    private void OnDisable()
    {
        if (animCtrl != null)
            animCtrl.OnAnimFinishEvent -= OnAnimationEvent;
    }
}
