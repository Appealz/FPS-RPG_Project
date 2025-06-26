using UnityEngine;

public interface IEnemyAttack
{
    void InitAttack();
    void AttackUpdate();
    void OnAttack();
    void OnAnimationEvent(string evt);
    void SetEnable(bool isOn);
}