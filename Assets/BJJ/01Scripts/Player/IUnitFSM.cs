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
    // ���� �������̽� ���� �÷��̾� �����Ʈ�� ���̴� ������ �����ҰŰ��׿�
    // Ȥ�� �̵� �������̽��� ������ �ż��带 �����ּ���
    IMovement movement;
    
    public JumpState(IMovement moveComponent)
    {
        movement = moveComponent;
    }

    public override void EnterState()
    {
        // todo jump�������̽� ��������� �߰�
    }

    public override void ExitState()
    {
        // todo jump�������̽� ��������� �߰�
    }
}

public class UseState : State
{
    // todo Use

    public override void EnterState()
    {
        // todo Use ��Ʈ�ѷ� �߰� �� �ż���
    }

    public override void ExitState()
    {
        // todo Use ��Ʈ�ѷ� �߰� �� �ż���
    }
}

public class ReloadState : State
{
    // todo Use ��Ʈ�ѷ� �߰� �� �߰�

    public override void EnterState()
    {
        
    }

    public override void ExitState()
    {
        
    }
}

public class SwapState : State
{
    // �÷��̾� �ι��丮? (���� ���� ����Ʈ) ��Ʈ�ѷ� �ϼ��Ǹ�
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