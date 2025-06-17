using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    PlayerInputAction inputAction;
    public event Action<Vector2> OnMoveInput;
    public event Action OnJumpInput;
    public event Action OnAttackInput;
    public event Action<Vector2> OnLookInput;
    private void Awake()
    {
        inputAction = new PlayerInputAction();
    }

    public void Init()
    {
        inputAction.Player.Enable();
        inputAction.Player.Move.performed += OnMovePerformed;
        inputAction.Player.Jump.performed += OnJumpPerformed;
        inputAction.Player.Look.performed += OnLookPerformed;
        
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

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        Vector2 lookDelta = context.ReadValue<Vector2>();
        OnLookInput?.Invoke(lookDelta);
    }
}
