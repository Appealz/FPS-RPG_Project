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
        // todo Use ��Ʈ�ѷ� �߰� �� �ż���
    }

    public void ExitState()
    {
        // todo Use ��Ʈ�ѷ� �߰� �� �ż���
    }
}

public class ReloadState : IState
{
    // todo Use ��Ʈ�ѷ� �߰� �� �߰�

    public void EnterState()
    {
        
    }

    public void ExitState()
    {
        
    }
}

public class SwapState : IState
{
    // �÷��̾� �ι��丮? (���� ���� ����Ʈ) ��Ʈ�ѷ� �ϼ��Ǹ�
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