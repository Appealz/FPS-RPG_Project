using UnityEngine;

public enum StateType
{ 
    Idle,
    Move,
    Jump,
    Use,
    Reload,
    Swap,
    Skill
}

public interface IUnitFSM
{
    void ResistState(StateType type, State state);
    void SetState(StateType type);
}

public interface IState
{
    void EnterState();
    void ExitState();
}

public abstract class State : IState
{

    public abstract void EnterState();

    public abstract void ExitState();
}

public class MoveState : State
{
    private IMovement movement;

    public MoveState(IMovement moveComponent)
    {
        movement = moveComponent;
    }

    public override void EnterState()
    {
        movement.SetEnable(true);
    }

    public override void ExitState()
    {
        movement.SetEnable(false);
    }
}

public class JumpState : State
{
    // 점프 인터페이스 만들어서 플레이어 무브먼트에 붙이는 구조로 가야할거같네요
    // 혹은 이동 인터페이스에 점프용 매서드를 열어주세요
    IMovement movement;
    
    public JumpState(IMovement moveComponent)
    {
        movement = moveComponent;
    }

    public override void EnterState()
    {
        // todo jump인터페이스 만들어지면 추가
    }

    public override void ExitState()
    {
        // todo jump인터페이스 만들어지면 추가
    }
}

public class UseState : State
{
    // todo Use

    public override void EnterState()
    {
        // todo Use 컨트롤러 추가 후 매서드
    }

    public override void ExitState()
    {
        // todo Use 컨트롤러 추가 후 매서드
    }
}

public class ReloadState : State
{
    // todo Use 컨트롤러 추가 후 추가

    public override void EnterState()
    {
        
    }

    public override void ExitState()
    {
        
    }
}

public class SwapState : State
{
    // 플레이어 인밴토리? (보유 무기 리스트) 컨트롤러 완성되면
    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }
}

public class SkillState : State
{
    IPlayerSkill skillCtrl;

    public SkillState(IPlayerSkill skillCtrl)
    {
        this.skillCtrl = skillCtrl;
    }

    public override void EnterState()
    {
        skillCtrl.SetEnable(true);
    }

    public override void ExitState()
    {
        skillCtrl.SetEnable(false);
    }
}