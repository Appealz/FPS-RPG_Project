using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAreaAttackMovement : MonoBehaviour, IMovement
{
    private IEnemyContextReadable context;
    private IAnimHandle animHandle;
    private NavMeshAgent agent;
    private bool isChase;

    private float curDelay;
    private float interval = 0.25f;

    public void Init()
    {
        if (!TryGetComponent<NavMeshAgent>(out agent))
            Debug.Log($"{gameObject.name} EnemyAreaAttackMovement.cs - Init() - Can't Reference NavMeshAgent");
        if (!TryGetComponent<IEnemyContextReadable>(out context))
            Debug.Log($"{gameObject.name} EnemyAreaAttackMovement.cs - Init() - Can't Reference Context");
        if (!TryGetComponent<IAnimHandle>(out animHandle))
            Debug.Log($"{gameObject} EnemyMovement.cs - Init() - Can't Reference IAnimHandle");

        curDelay = 0f;
        isChase = false;
    }

    public void Move()
    {
        if (context.curTarget == null || !context.curTarget.IsAlive) return;

        agent.speed = context.moveSpeed;
        if (NavMesh.SamplePosition(RandomTargetNearPos(), out var hit, 1f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        else
            curDelay = 0.05f;
    }

    private Vector3 RandomTargetNearPos()
    {
        Vector2 randomFactor = Random.insideUnitCircle * context.attackRange;
        Vector3 basePos = context.curTarget.GetTransform().position;
        return new Vector3(basePos.x + randomFactor.x, basePos.y, basePos.z + randomFactor.y);
    }

    public void MoveUpdate()
    {
        if (!isChase) return;

        curDelay -= Time.deltaTime;
        if (curDelay <= 0f)
        {
            curDelay = interval;
            Move();
        }
    }

    public void SetDirection(Vector2 dir)
    {
        
    }

    public void SetEnable(bool isOn)
    {
        isChase = isOn;
        agent.isStopped = !isOn;
        curDelay = 0f;

        animHandle.SetMoveState(isChase);
    }
}
