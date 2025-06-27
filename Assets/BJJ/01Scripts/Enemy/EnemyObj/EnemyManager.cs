using UnityEngine;

public interface IEnemyManager
{
    void InitEnemy();
    void CustomUpdate();
}

[RequireComponent(typeof(EnemyContext))]
public class EnemyManager : MonoBehaviour, IPoolLabel, IEnemyManager, IDamageReceiver
{
    private Pool ownerPool;
    private StatManager statManager;
    private IEnemyAI enemyAI;
    private EnemyContext enemyContext;
    private IUnitFSM unitFSM;
    private IMovement movement;
    private IEnemyAttack attackCtrl;
    // todo 몬스터 애니메이터 이벤트
    
    public void Create(Pool onwerPool)
    {
        this.ownerPool = onwerPool;
        gameObject.SetActive(false);
    }

    // todo 데이터 매니져가 만들어지면 데이터를 받아오는 형태로 만들어질 예정
    public void InitEnemy()
    {
        // 추후에 데이터 매니져가 만들어져서 스텟을 받아서 초기화가 가능하면 그때 수정
        statManager = new StatManager(gameObject, null, null);

        if(!TryGetComponent<EnemyContext>(out enemyContext))
            Debug.Log($"{gameObject.name} EnemyManager.cs - InitEnemy() - EnemyContext NonReference");

        if (!TryGetComponent<IEnemyAttack>(out attackCtrl))
            Debug.Log($"{gameObject.name} EnemyManager.cs - InitEnemy() - EnemyContext NonReference");
        else attackCtrl.InitAttack();

        if (!TryGetComponent<IMovement>(out movement))
            Debug.Log($"{gameObject.name} EnemyManager.cs - InitEnemy() - IUnitFSM NonReference");
        else
            movement.Init();

        if (!TryGetComponent<IUnitFSM>(out unitFSM))
            Debug.Log($"{gameObject.name} EnemyManager.cs - InitEnemy() - IUnitFSM NonReference");
        else
        {
            unitFSM.ResistState(StateType.Idle, new IdleState());
            unitFSM.ResistState(StateType.Move, new MoveState(movement));
            unitFSM.ResistState(StateType.Attack, new AttackState(attackCtrl));
        }

        if(!TryGetComponent<IEnemyAI>(out enemyAI))
            Debug.Log($"{gameObject} EnemyManager.cs - InitEnemy() - IEnemyAI NonReference");
        else
        {
            enemyAI.InitAI(unitFSM, enemyContext);
        }

        HitPart[] hitparts = transform.GetComponentsInChildren<HitPart>();
        foreach(var part in hitparts)
        {
            if (part.name.Contains("head"))
                part.Init(this, DamageReceivePart.Head);
            else part.Init(this, DamageReceivePart.Body);
        }

        EventBus_Stat.Subscribe(OnStatChange);
        EventBus_UnitDieEvent.Subscribe(DieEventHandler);
    }

    public void CustomUpdate()
    {
        enemyAI.AIUpdate();
        movement.MoveUpdate();
        attackCtrl.AttackUpdate();
    }

    public void ReturnToPool()
    {
        EventBus_Stat.Unsubscribe(OnStatChange);
        EventBus_UnitDieEvent.UnSubscribe(DieEventHandler);
        ownerPool.ReturnToPool(gameObject);
    }

    private void OnStatChange(StatModifier modifier)
    {
        if (modifier.sender != gameObject) return;

        switch(modifier.EventType)
        {
            case StatEventType.Add:
                statManager.AddModifier(modifier.ChangedStatType, modifier.ChangeValue, modifier.isMulti);
                break;
            case StatEventType.Remove:
                statManager.RemoveModifier(modifier.ChangedStatType, modifier.ChangeValue, modifier.isMulti);
                break;
        }

        enemyContext.StatUpdate(statManager);
    }

    private void DieEventHandler(UnitDieEvent dieEvent)
    {
        if (dieEvent.sender != gameObject) return;

        EventBus_EnemyManager.Publish(new EnemyUpdateEvent(EnemyUpdateType.Unregist, this));
        ReturnToPool();
    }

    public void OnHit(DamageReceivePart part, DamageInfo info)
    {
        float resultDamage = info.damage;

        if (part == DamageReceivePart.Head)
            resultDamage *= 1.5f;

        statManager.ApplyDamage(resultDamage);
        statManager.AddBuff(info.buff);
    }

    public void OnExplosionDamageHandler(GameObject sender, DamageInfo info)
    {
        if (sender != gameObject) return;

        statManager.ApplyDamage(info.damage);
        statManager.AddBuff(info.buff);
    }
}
