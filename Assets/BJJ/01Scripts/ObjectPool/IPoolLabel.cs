using UnityEngine;

public interface IPoolLabel
{
    void Create(Pool onwerPool);// 파라미터로 풀 받아야함
    void ReturnToPool();
}

