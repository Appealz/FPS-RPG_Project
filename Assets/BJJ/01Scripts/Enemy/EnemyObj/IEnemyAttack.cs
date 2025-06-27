using UnityEngine;

public interface IEnemyAttack
{
    void InitAttack();
    void AttackUpdate();
    void OnAttack();
    void OnAnimationEvent();
    void SetEnable(bool isOn);
}