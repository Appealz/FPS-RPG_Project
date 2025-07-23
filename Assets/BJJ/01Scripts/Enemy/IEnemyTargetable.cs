using System.Collections.Generic;
using UnityEngine;
public interface IEnemyTargetable : ITargetable
{
    IReadOnlyList<Transform> Parts { get; }
}
