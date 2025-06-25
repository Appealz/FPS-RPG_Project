using UnityEngine;

public interface IEnemyAttack
{
    void InitAttack();
    void AttackUpdate();
    void OnAttack();
    void SetEnable(bool isOn);
}