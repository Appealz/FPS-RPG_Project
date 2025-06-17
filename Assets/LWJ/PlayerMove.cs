using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour,IMovement
{
    private bool isMove;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;
    private Vector3 MoveDir;
    private Rigidbody rb;
    
        
    
    private void Awake()
    {
        if(!TryGetComponent<Rigidbody>(out rb))
        {
            Debug.Log("rb is not ref");
        }
    }

    public void Init()
    {
        isMove = true;
    }

    public void Move()
    {
        rb.linearVelocity = MoveDir * moveSpeed;
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up, ForceMode.Impulse);
    }

    public void SetDirection(Vector2 dir)
    {
        MoveDir = new Vector3(dir.x, 0f, dir.y);
    }

    public void MoveUpdate()
    {
        if (!isMove)
            return;
        Move();
    }

    public void SetEnable(bool isOn)
    {
        isMove = isOn;
    }

}
