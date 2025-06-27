using System;
using System.Linq;
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
    public event Action<int> OnEquipInput;
    public event Action OnReloadInput;

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
        inputAction.Player.EquipItem.performed += OnEquipItem;
        inputAction.Player.Attack.performed += OnAttackPerformed;
        inputAction.Player.Attack.canceled += OnAttackCanceled;
        inputAction.Player.Skill.performed += OnSkillPerformed;
        inputAction.Player.Reload.performed += OnReloadPerformed;
    }

    private void OnDisable()
    {
        inputAction.Player.Disable();
        inputAction.Player.Move.performed -= OnMovePerformed;
        inputAction.Player.Move.canceled -= OnMoveCanceled;
        inputAction.Player.Jump.performed -= OnJumpPerformed;
        inputAction.Player.Look.performed -= OnLookPerformed;
        inputAction.Player.EquipItem.performed -= OnEquipItem;
        inputAction.Player.Attack.performed -= OnAttackPerformed;
        inputAction.Player.Attack.canceled -= OnAttackCanceled;
        inputAction.Player.Skill.performed -= OnSkillPerformed;
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

    private void OnEquipItem(InputAction.CallbackContext context)
    {
        int index = context.action.GetBindingIndexForControl(context.control);
        OnEquipInput?.Invoke(index);
        OnStateChangeEvent?.Invoke(StateGroup.Attack, StateType.Swap);
        //Debug.Log($"{index}");
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        OnStateChangeEvent?.Invoke(StateGroup.Attack, StateType.Use);
        Debug.Log("공격 키 입력");
    }
    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        OnStateChangeEvent?.Invoke(StateGroup.Attack, StateType.Idle);
        Debug.Log("공격 종료");
    }
    private void OnReloadPerformed(InputAction.CallbackContext context)
    {
        OnReloadInput?.Invoke();        
    }

    private void OnSkillPerformed(InputAction.CallbackContext context)
    {
        OnStateChangeEvent?.Invoke(StateGroup.Attack, StateType.Skill);
    }
}
