using UnityEngine;

public interface IItem
{
    // todo : SO ����.
    void InitData();
    void Use();
    bool useable { get; }
}