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
        Vector3 newVelocity = MoveDir * moveSpeed;
        newVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = newVelocity;
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
