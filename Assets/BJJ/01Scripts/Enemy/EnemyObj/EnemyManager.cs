using System.Collections.Generic;
using UnityEngine;

public interface IEnemyManager
{
    void InitEnemy(EnemyData newData);
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
    private IAnimCtrl animCtrl;

    public GameObject ReciverGO => gameObject;

    public void Create(Pool onwerPool)
    {
        this.ownerPool = onwerPool;
        gameObject.SetActive(false);
    }

    public void InitEnemy(EnemyData n)
    {
        statManager = new StatManager(gameObject, new Dictionary<StatType, StatValue>()
        {
            {StatType.HP, new StatValue(n.maxHp)},
            {StatType.AttackDamage, new StatValue(n.damage) },
            {StatType.MoveSpeed, new StatValue(n.moveSpeed) },
            {StatType.AttackSpeed, new StatValue(n.attackSpeed) },
            {StatType.AttackRange, new StatValue(n.range) }
        });

        if (!TryGetComponent<IAnimCtrl>(out animCtrl))
            Debug.Log($"{gameObject.name} EnemyManager.cs - InitEnemy() - IAnimCtrl NonReference");
        else animCtrl.Init();

        if (!TryGetComponent<EnemyContext>(out enemyContext))
            Debug.Log($"{gameObject.name} EnemyManager.cs - InitEnemy() - EnemyContext NonReference");
        else enemyContext.StatUpdate(statManager);

        if (!TryGetComponent<IEnemyAttack>(out attackCtrl))
            Debug.Log($"{gameObject.name} EnemyManager.cs - InitEnemy() - IEnemyAttack NonReference");
        else attackCtrl.InitAttack(EnemyWeaponFactory.GetEnemyWeapon(n.name));

        if (!TryGetComponent<IMovement>(out movement))
            Debug.Log($"{gameObject.name} EnemyManager.cs - InitEnemy() - IMovement NonReference");
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

        IHitPart[] hitparts = transform.GetComponentsInChildren<IHitPart>();
        foreach(var part in hitparts)
        {
            part.Init(this);
        }

        EventBus_Stat.Subscribe(OnStatChange);
        EventBus_Damage.SubScribe(OnExplosionDamageHandler);
        EventBus_UnitDieEvent.Subscribe(DieEventHandler);
    }

    public void CustomUpdate()
    {
        animCtrl.AnimUpdate();
        enemyAI.AIUpdate();
        movement.MoveUpdate();
        attackCtrl.AttackUpdate();
    }

    public void ReturnToPool()
    {
        EventBus_Stat.Unsubscribe(OnStatChange);
        EventBus_Damage.UnSubscribe(OnExplosionDamageHandler);
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

    public void OnExplosionDamageHandler(DamageInfo info)
    {
        if (info.receiver != gameObject) return;

        statManager.ApplyDamage(info.damage);
        statManager.AddBuff(info.buff);
    }
}
