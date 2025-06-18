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
    { StateType.Idle, StateType.Move, StateType.Jump};
    private readonly HashSet<StateType> attackStateSet = new()
    { StateType.Idle, StateType.Use, StateType.Reload, StateType.Swap, StateType.Skill };

    public void ResistState(StateType type, IState state)
    {
        if (moveStateSet.Contains(type))
            stateMap[StateGroup.Move][type] = state;
        if (attackStateSet.Contains(type))
            stateMap[StateGroup.Attack][type] = state;
    }

    public void SetState(StateGroup group, StateType type)
    {
        if(stateMap.TryGetValue(group, out var stateList))
        {
            if (curStateMap[group] != null)
                curStateMap[group].ExitState();

            stateList[type].EnterState();
            curStateMap[group] = stateList[type];
        }
    }
}
// 서브 FSM을 만들어서 이통합 컨트롤러에 넣고
// 여기서 받아서 서브에게 넘겨주는 구조로 등록
// 상태 변화는 여기서 매서드 enum하나 더 만들어서
// GameObjgect sender , StateGroup group, StateType type