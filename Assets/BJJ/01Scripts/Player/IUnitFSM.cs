using UnityEngine;

public enum StateType
{ 
    Idle,
    Move,
    Use,
    Reload,
    Swap,
    Skill
}

public interface IUnitFSM
{
    void ResistState(StateType type, IState state);
    void SetState(StateGroup group, StateType type);
}

public interface IState
{
    void EnterState();
    void ExitState();
}

public class IdleState : IState
{
    public void EnterState()
    {
        
    }

    public void ExitState()
    {
        
    }
}

public class MoveState : IState
{
    private IMovement movement;

    public MoveState(IMovement moveComponent)
    {
        movement = moveComponent;
    }

    public void EnterState()
    {
        movement.SetEnable(true);
    }

    public void ExitState()
    {
        movement.SetEnable(false);
    }
}

public class UseState : IState
{
    // todo Use

    public void EnterState()
    {
        // todo Use 컨트롤러 추가 후 매서드
    }

    public void ExitState()
    {
        // todo Use 컨트롤러 추가 후 매서드
    }
}

public class ReloadState : IState
{
    // todo Use 컨트롤러 추가 후 추가

    public void EnterState()
    {
        
    }

    public void ExitState()
    {
        
    }
}

public class SwapState : IState
{
    // 플레이어 인밴토리? (보유 무기 리스트) 컨트롤러 완성되면
    public void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public void ExitState()
    {
        throw new System.NotImplementedException();
    }
}

public class SkillState : IState
{
    IPlayerSkill skillCtrl;

    public SkillState(IPlayerSkill skillCtrl)
    {
        this.skillCtrl = skillCtrl;
    }

    public void EnterState()
    {
        skillCtrl.SetEnable(true);
    }

    public void ExitState()
    {
        skillCtrl.SetEnable(false);
    }
}