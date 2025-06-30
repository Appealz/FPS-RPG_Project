using System;
using UnityEngine;

public interface IAnimHandle
{
    void SetAnim(string anim);
    void SetMoveState(bool isOn);

    event Action OnAttackEvent;
    event Action OnAnimFinishEvent;
}

public interface IAnimCtrl
{
    void Init();
    void AnimUpdate();
    void ExecuteAnimEvent(TimeEvent evt);
}
