using UnityEngine;

public interface IItem
{
    // todo : SO ажют.
    void InitData();
    void Use();
    bool useable { get; }
}