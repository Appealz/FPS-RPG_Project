using UnityEngine;

public interface IEnemyAI
{
    void InitAI(IUnitFSM fsm, IEnemyContextWriteable context);

    void AIUpdate();
}

public class EnemyAI : MonoBehaviour, IEnemyAI
{
    private IUnitFSM fsm;
    private IEnemyContextWriteable context;

    public void InitAI(IUnitFSM fsm, IEnemyContextWriteable context)
    {
        this.fsm = fsm;
        this.context = context;
    }

    public void AIUpdate()
    {
        if (context.curTarget == null || !context.curTarget.IsAlive)
            context.SetTarget(PlayerScanManager.Instance.FindNearst(transform.position));

        if ((context.curTarget.GetTransform().position - transform.position).sqrMagnitude > context.attackRange * context.attackRange)
            fsm.SetState(StateGroup.Enemy, StateType.Move);
        else
            fsm.SetState(StateGroup.Enemy, StateType.Attack);
    }
}
