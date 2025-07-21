using System.Collections.Generic;
using UnityEngine;

public class SentryGunAttackState : IState
{
    private ISentryGunWeapon weapon;

    public SentryGunAttackState(ISentryGunWeapon weapon)
    {
        this.weapon = weapon;
    }

    public void EnterState()
    {
        weapon.OnAttack();
    }

    public void ExitState()
    {
        weapon.OffAttack();
    }
}

public class SentryGunFSM : MonoBehaviour, IUnitFSM
{
    private Dictionary<StateType, IState> stateList = new Dictionary<StateType, IState>();
    private IState curState;

    public void ResistState(StateType type, IState state)
    {
        stateList[type] = state;
    }

    public void SetState(StateGroup group, StateType type)
    {
        if (stateList.TryGetValue(type, out IState value))
        {
            if (curState != null)
                curState.ExitState();

            curState = value;
            curState.EnterState();
        }
        else
            Debug.Log("SentryGunFSM.cs - SetState() - ErrorStateType");
    }
}
