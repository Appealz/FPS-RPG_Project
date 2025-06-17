using UnityEngine;

public interface IMovement
{
    void MoveUpdate();
    void Move();
    void SetEnable(bool isOn);
    void Init();

}