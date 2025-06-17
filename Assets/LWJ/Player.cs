using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerMove playerMove;
    PlayerInputController inputController;

    private void Awake()
    {
        if(!TryGetComponent<PlayerMove>(out playerMove))
        {
            Debug.Log("playerMove is not ref");
        }
        if (!TryGetComponent<PlayerInputController>(out inputController))
        {
            Debug.Log("inputController is not ref");
        }
        inputController.Init();
        inputController.OnMoveInput += playerMove.SetDirection;
        inputController.OnJumpInput += playerMove.Jump;
        playerMove.Init();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMove.MoveUpdate();
    }
}
