using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    PlayerInputAction inputAction;
    public event Action<Vector2> OnMoveInput;
    public event Action OnJumpInput;
    public event Action OnAttackInput;
    public event Action<Vector2> OnLookInput;

    public event Action<StateGroup, StateType> OnStateChangeEvent;
    private void Awake()
    {
        inputAction = new PlayerInputAction();
    }

    public void Init()
    {
        inputAction.Player.Enable();
        inputAction.Player.Move.performed += OnMovePerformed;
        inputAction.Player.Move.canceled += OnMoveCanceled;
        inputAction.Player.Jump.performed += OnJumpPerformed;
        inputAction.Player.Look.performed += OnLookPerformed;        
    }

    private void OnDisable()
    {
        inputAction.Player.Disable();
        inputAction.Player.Move.performed -= OnMovePerformed;
        inputAction.Player.Jump.performed -= OnJumpPerformed;
        inputAction.Player.Look.performed -= OnLookPerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 inputDir = context.ReadValue<Vector2>();        
        OnMoveInput?.Invoke(inputDir);
        OnStateChangeEvent?.Invoke(StateGroup.Move, StateType.Move);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        //Vector2 inputDir = Vector2.zero;
        //OnMoveInput?.Invoke(inputDir);
        OnStateChangeEvent?.Invoke(StateGroup.Move, StateType.Idle);
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
