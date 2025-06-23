using UnityEngine;

[RequireComponent(typeof(EnemyContext))]
public class EnemyManager : MonoBehaviour, IPoolLabel
{
    private Pool ownerPool;
    private StatManager statManager;
    private IEnemyAI enemyAI;
    private EnemyContext enemyContext;
    private IUnitFSM unitFSM;
    private IMovement movement;
    
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
            Debug.Log($"{gameObject} EnemyManager.cs - InitEnemy() - EnemyContext NonReference");

        if (!TryGetComponent<IMovement>(out movement))
            Debug.Log($"{gameObject} EnemyManager.cs - InitEnemy() - IUnitFSM NonReference");
        else
            movement.Init();

        if (!TryGetComponent<IUnitFSM>(out unitFSM))
            Debug.Log($"{gameObject} EnemyManager.cs - InitEnemy() - IUnitFSM NonReference");
        else
        {
            unitFSM.ResistState(StateType.Idle, new IdleState());
            unitFSM.ResistState(StateType.Move, new MoveState(movement));
            unitFSM.ResistState(StateType.Attack, new AttackState());
            // todo 공격 및 생성 상태
        }

        if(!TryGetComponent<IEnemyAI>(out enemyAI))
            Debug.Log($"{gameObject} EnemyManager.cs - InitEnemy() - IEnemyAI NonReference");
        else
        {
            enemyAI.InitAI(unitFSM, enemyContext);
        }

        EventBus_Stat.Subscribe(OnStatChange);
    }

    private void CustomUpdate()
    {
        enemyAI.AIUpdate();
        movement.MoveUpdate();
    }

    public void ReturnToPool()
    {
        ownerPool.ReturnToPool(gameObject);
    }

    private void OnDisable()
    {
        EventBus_Stat.Unsubscribe(OnStatChange);
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
}
