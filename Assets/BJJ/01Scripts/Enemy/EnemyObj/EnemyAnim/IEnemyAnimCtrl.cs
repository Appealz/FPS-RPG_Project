using System;
using UnityEngine;

public interface IAnimHandle
{
    void SetAnim(string anim);
    void SetMoveState(bool isOn);
}

public interface IAnimCtrl
{
    void Init();
    void AnimUpdate();
    void ExecuteAnimEvent();
}
