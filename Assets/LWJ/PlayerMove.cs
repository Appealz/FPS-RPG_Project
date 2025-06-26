using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour,IMovement, IJump, ISetDirection
{
    private bool isMove;
    private int jumpCount;

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
        jumpCount = 1;
    }

    public void Move()
    {        
        Vector3 newVelocity = MoveDir * moveSpeed;
        newVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = newVelocity;
    }

    public void Jump()
    {
        //if (jumpCount == 0)
        //    return;        
        //jumpCount--;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void SetDirection(Vector2 dir)
    {        
        MoveDir = transform.forward * dir.y + transform.right * dir.x;
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
