using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    PlayerInputAction inputAction;
    public event Action<Vector2> OnMoveInput;
    public event Action OnJumpInput;
    public event Action OnAttackInput;
    private void Awake()
    {
        inputAction = new PlayerInputAction();
    }

    public void Init()
    {
        inputAction.Player.Enable();
        inputAction.Player.Move.performed += OnMovePerformed;
        inputAction.Player.Jump.performed += OnJumpPerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 inputDir = context.ReadValue<Vector2>();        
        OnMoveInput?.Invoke(inputDir);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnJumpInput.Invoke();
    }
}
