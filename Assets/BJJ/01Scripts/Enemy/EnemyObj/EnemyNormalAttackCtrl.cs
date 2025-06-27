using UnityEngine;

public class EnemyNormalAttackCtrl : MonoBehaviour, IEnemyAttack
{
    private IEnemyContextReadable context;
    private IAnimHandle animCtrl;
    private IEnemyWeapon weapon;

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
        if (!TryGetComponent<IAnimHandle>(out animCtrl))
            Debug.Log($"{gameObject.name} EnemyAttackSuidideAttackCtrl.cs - InitAttack() - Can't Reference nimCtrl");

        interval = context.attackSpeed;
        curDelay = 0;
        isAttackState = false;
    }

    public void OnAnimationEvent()
    {
        // todo 애니메이션 이벤트 버스 만들어진 뒤에 받아오기
    }

    public void OnAttack()
    {
        isAttackable = false;
        animCtrl.SetAnim("OnAttack");
    }

    public void SetEnable(bool isOn)
    {
        isAttackState = isOn;
        isAttackable = isOn;
        curDelay = 0;
    }
}
