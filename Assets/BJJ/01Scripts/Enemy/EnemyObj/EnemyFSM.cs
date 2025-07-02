using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour, IUnitFSM
{
    private Dictionary<StateType, IState> fsmDIc = new Dictionary<StateType, IState>();
    private IState curState;

    public void ResistState(StateType type, IState state)
    {
        fsmDIc[type] = state;
    }

    public void SetState(StateGroup group, StateType type)
    {
        if (group != StateGroup.Enemy) return;

        if(fsmDIc.TryGetValue(type, out var state))
        {

            if (curState != null)
            {
                if (curState == state) return;

                curState.ExitState();
            }

            curState = state;
            curState.EnterState();
        }
    }
}
