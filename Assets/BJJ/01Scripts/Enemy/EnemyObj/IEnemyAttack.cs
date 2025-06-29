using UnityEngine;

public interface IEnemyAttack
{
    void InitAttack(IEnemyWeapon newWeapon);
    void AttackUpdate();
    void OnAttack();
    void OnAnimationEvent();
    void SetEnable(bool isOn);
}