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
    /// 인터페이스로 만들 필요가 있겠네요
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
// 이동을 할때
// 입력 -> 이동 상태 전환 -> 이동
// 이 입력과 이동 상태의 전환 이 사이의 통신 구조
// 일반적으로 이러한 부분들은 플래그로 관리하잖아요?
// 근데 이 플래그들은 상태 객체에서 변경하도록 구조를 설계했어요
// 