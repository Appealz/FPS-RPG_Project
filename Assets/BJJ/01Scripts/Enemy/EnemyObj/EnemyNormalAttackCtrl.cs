using UnityEngine;

public class EnemyNormalAttackCtrl : MonoBehaviour, IEnemyAttack
{
    private IEnemyContextReadable context;

    private bool isAttackState;
    private bool isAttackable;

    private float interval;
    private float curDelay;

    public void AttackUpdate()
    {
        if (!isAttackState) return;
        if (!isAttackable) return;
        
        curDelay -= Time.deltaTime;

        if(curDelay <= 0f)
        {
            curDelay = interval;
            OnAttack();
        }
    }

    public void InitAttack()
    {
        if (!TryGetComponent<IEnemyContextReadable>(out context))
            Debug.Log($"{gameObject.name} EnemyNormalAttackCtrl.cs - InitAttack() - Context Don't Reference");

        interval = context.attackSpeed;
        curDelay = 0;
        isAttackState = false;
    }

    public void OnAttack()
    {
        // 이벤트 버스 애니메이션 작동?
        isAttackable = false;
    }

    public void SetEnable(bool isOn)
    {
        isAttackState = isOn;
        isAttackable = isOn;
        curDelay = 0;
    }
}
