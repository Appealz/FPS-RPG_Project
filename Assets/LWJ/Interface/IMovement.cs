using UnityEngine;

public interface IMovement
{
    void MoveUpdate();
    void Move();
    void SetEnable(bool isOn);
    void SetDirection(Vector2 dir);
    void Init();
}