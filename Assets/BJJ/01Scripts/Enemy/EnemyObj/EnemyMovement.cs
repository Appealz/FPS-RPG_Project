using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour, IMovement
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
            Debug.Log($"{gameObject} EnemyMovement.cs - Init() - Can't Reference NavMeshAgent");
        if (!TryGetComponent<IEnemyContextReadable>(out context))
            Debug.Log($"{gameObject} EnemyMovement.cs - Init() - Can't Reference Context");
        if(!TryGetComponent<IAnimHandle>(out animHandle))
            Debug.Log($"{gameObject} EnemyMovement.cs - Init() - Can't Reference IAnimHandle");

        curDelay = 0f;
        isChase = false;
    }

    public void Move()
    {
        if (context.curTarget == null) return; //  || !context.curTarget.IsAlive �߰��ؾ���

        agent.speed = context.moveSpeed;
        agent.SetDestination(context.curTarget.GetTransform().position);
    }

    public void MoveUpdate()
    {
        if (!isChase) return;
        curDelay -= Time.deltaTime;
        if(curDelay <= 0f)
        {
            curDelay = interval;
            Move();
        }
    }

    // ���ö�Ʈ �� ���� ���� (�÷��̾���� ����ϴ� �ż���)
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

