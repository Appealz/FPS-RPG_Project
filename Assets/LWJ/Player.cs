using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    IMovement playerMove;
    PlayerInputController inputController;
    CameraController cameraController;
    IUnitFSM playerFSM;
    IItemCtrl itemCtrl;
    PlayerDataManager dataManager;
    private void Awake()
    {
        if(!TryGetComponent<IMovement>(out playerMove))
        {
            Debug.Log("playerMove is not ref");
        }
        if (!TryGetComponent<PlayerInputController>(out inputController))
        {
            Debug.Log("inputController is not ref");
        }
        if(!TryGetComponent<CameraController>(out cameraController))
        {
            Debug.Log("camera is not ref");
        }
        if(!TryGetComponent<PlayerDataManager>(out dataManager))
        {
            Debug.Log("dataManager is not ref");
        }
        dataManager.InitPlayerData();
        if(!TryGetComponent<IUnitFSM>(out playerFSM))
        {
            Debug.Log("playerFSM is not ref");
        }
        if(!TryGetComponent<IItemCtrl>(out itemCtrl))
        {
            Debug.Log("itemCtrl is not ref");
        }
        itemCtrl.Init();

        playerFSM.ResistState(StateType.Idle, new IdleState());
        playerFSM.ResistState(StateType.Move, new MoveState(playerMove));

        #region _KeyBinding_
        inputController.Init();
        inputController.OnStateChangeEvent += playerFSM.SetState;
        inputController.OnMoveInput += playerMove.SetDirection;
        if(playerMove is IJump jump)
        {
            inputController.OnJumpInput += jump.Jump;
        }        
        inputController.OnLookInput += cameraController.UpdateRotate;
        inputController.OnEquipInput += dataManager.inventory.EquipItem;
        inputController.OnAttackInput += itemCtrl.UseCurrentItem;
        #endregion

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
        itemCtrl.UseCurrentItem();
    }
}
