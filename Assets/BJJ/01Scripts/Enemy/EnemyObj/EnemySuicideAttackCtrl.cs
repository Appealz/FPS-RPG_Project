using UnityEngine;

public class EnemySuicideAttackCtrl : MonoBehaviour, IEnemyAttack
{
    private IEnemyContextReadable context;
    private IAnimHandle animCtrl;
    private bool isAttack;

    public void AttackUpdate()
    {
        if (!isAttack) return;

        OnAttack();
    }

    public void InitAttack()
    {
        if (!TryGetComponent<IEnemyContextReadable>(out context))
            Debug.Log($"{gameObject.name} EnemyAttackSuidideAttackCtrl.cs - InitAttack() - Can't Reference EnemyContext");
        if(!TryGetComponent<IAnimHandle>(out animCtrl))
            Debug.Log($"{gameObject.name} EnemyAttackSuidideAttackCtrl.cs - InitAttack() - Can't Reference nimCtrl");
        else animCtrl.OnAnimEvent += OnAnimationEvent;

        isAttack = false;
    }

    public void OnAnimationEvent(string evt)
    {
        // todo 이벤트 타입들이 정해지면 그에 맞춰서 작동
    }

    public void OnAttack()
    {
        // todo 자폭 공격
    }

    public void SetEnable(bool isOn)
    {
        isAttack = isOn;
    }

    private void OnDisable()
    {
        animCtrl.OnAnimEvent -= OnAnimationEvent;
    }
}
