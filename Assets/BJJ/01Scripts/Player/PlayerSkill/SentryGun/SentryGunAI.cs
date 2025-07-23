using UnityEngine;

public interface ISentryGunAI
{
    void Init(IUnitFSM newFSM);

    void AIUpdate();
}

public class SentryGunAI : MonoBehaviour, ISentryGunAI
{
    private IUnitFSM fsm;
    private ISentryGunWriteableContext context;

    public void AIUpdate()
    {
        if (context.target == null || !context.target.IsAlive)
            TrySetTarget();
        
        if(IsErrorTarget())
        {
            fsm.SetState(StateGroup.Enemy, StateType.Idle);
            context.NewTarget(null);
        }
    }

    public void Init(IUnitFSM newFSM)
    {
        fsm = newFSM;
        if(!TryGetComponent<ISentryGunWriteableContext>(out context))
        {
            Debug.Log("SentryGunAI.cs - Init() - ISentryGunContext Can't Find");
        }
        fsm.SetState(StateGroup.Enemy, StateType.Idle);
    }

    private void TrySetTarget()
    {
        Collider[] enemys = Physics.OverlapSphere(transform.position, context.GetStat(StatType.AttackRange), LayerMask.GetMask("Enemy"));

        if(enemys.Length < 1)
        {
            return;
        }

        IEnemyTargetable closetTarget = null;
        float closetSqrDis = float.MaxValue;
        foreach (Collider t in enemys)
        {
            if (!t.TryGetComponent<IEnemyTargetable>(out var targetable))
                continue;
            if (!targetable.IsAlive) continue;

            float sqrDis = (targetable.GetTransform().position - transform.position).sqrMagnitude;
            if(sqrDis < closetSqrDis)
            {
                closetSqrDis = sqrDis;
                closetTarget = targetable;
            }
        }

        if (closetTarget == null) return;

        context.NewTarget(closetTarget);
        fsm.SetState(StateGroup.Enemy, StateType.Attack);
    }

    private bool IsErrorTarget()
    {
        float sqrDis = (context.target.GetTransform().position - transform.position).sqrMagnitude;
        float range = context.GetStat(StatType.AttackRange);
        return sqrDis > range * range;
    }
}
