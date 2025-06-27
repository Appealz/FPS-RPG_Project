using System;
using UnityEngine;

public interface IAnimHandle
{
    void SetAnim(string anim);
}

public interface IAnimCtrl
{
    void Init();
    void AnimUpdate();
    void ExecuteAnimEvent();
}
