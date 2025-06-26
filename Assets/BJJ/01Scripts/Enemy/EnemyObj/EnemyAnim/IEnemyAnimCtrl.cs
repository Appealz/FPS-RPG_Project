using System;
using UnityEngine;

public interface IAnimHandle
{
    void SetAnim(int animHash);
    event Action<string> OnAnimEvent;
}

public interface IEnemyAnimCtrl : IAnimHandle
{
    void Init();
    void AnimUpdate();
    void CheckAnimEvent();
}
