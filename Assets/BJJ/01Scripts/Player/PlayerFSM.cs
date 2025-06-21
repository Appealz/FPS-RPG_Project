using System.Collections.Generic;
using UnityEngine;

public enum StateGroup
{
    Move,
    Attack,
    Enemy
}

public class PlayerFSM : MonoBehaviour, IUnitFSM
{
    private Dictionary<StateGroup, Dictionary<StateType, IState>> stateMap = new()
    {
        {StateGroup.Move, new Dictionary<StateType, IState>() },
        {StateGroup.Attack, new Dictionary<StateType, IState>() }
    };

    private Dictionary<StateGroup, IState> curStateMap = new()
    {
        {StateGroup.Move, null},
        {StateGroup.Attack, null }
    };

    private readonly HashSet<StateType> moveStateSet = new()
    { StateType.Idle, StateType.Move};
    private readonly HashSet<StateType> attackStateSet = new()
    { StateType.Idle, StateType.Use, StateType.Reload, StateType.Swap, StateType.Skill };

    private bool isAttackable = true;

    /// <summary>
    /// �������̽��� ���� �ʿ䰡 �ְڳ׿�
    /// </summary>
    public void InitFSM()
    {
        isAttackable = true;
    }

    public void ResistState(StateType type, IState state)
    {
        if (moveStateSet.Contains(type))
            stateMap[StateGroup.Move][type] = state;
        if (attackStateSet.Contains(type))
            stateMap[StateGroup.Attack][type] = state;
    }

    public void SetState(StateGroup group, StateType type)
    {
        if(group == StateGroup.Attack)
        {
            if (!isAttackable) return;
        }

        if(stateMap.TryGetValue(group, out var stateList))
        {
            if (curStateMap[group] != null)
                curStateMap[group].ExitState();

            stateList[type].EnterState();
            curStateMap[group] = stateList[type];
        }
    }
}
// �̵��� �Ҷ�
// �Է� -> �̵� ���� ��ȯ -> �̵�
// �� �Է°� �̵� ������ ��ȯ �� ������ ��� ����
// �Ϲ������� �̷��� �κе��� �÷��׷� �������ݾƿ�?
// �ٵ� �� �÷��׵��� ���� ��ü���� �����ϵ��� ������ �����߾��
// 