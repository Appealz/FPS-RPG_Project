using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour,ITargetable
{
    IMovement playerMove;
    PlayerInputController inputController;
    CameraController cameraController;
    IUnitFSM playerFSM;
    IItemCtrl itemCtrl;
    PlayerDataManager dataManager;
    IPlayerSkill playerSkill;

    [SerializeField]
    ClassSkillData skillData;

    public bool IsAlive => dataManager.statManager.CurHP <= 0;

    public Transform GetTransform()
    {
        return transform;
    }

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
        if(!TryGetComponent<IPlayerSkill>(out playerSkill))
        {

        }
        playerSkill.InitSkillCtrl(skillData);

        itemCtrl.Init();

        
        playerFSM.ResistState(StateType.Idle, new IdleState());
        playerFSM.ResistState(StateType.Move, new MoveState(playerMove));
        playerFSM.ResistState(StateType.Use, new UseState(itemCtrl));
        playerFSM.ResistState(StateType.Skill, new SkillState(playerSkill));

        #region _KeyBinding_
        inputController.Init();
        inputController.OnStateChangeEvent += playerFSM.SetState;        
        if(playerMove is ISetDirection direction)
        {
            inputController.OnMoveInput += direction.SetDirection;
        }
        if(playerMove is IJump jump)
        {
            inputController.OnJumpInput += jump.Jump;
        }
        inputController.OnLookInput += cameraController.UpdateRotate;
        inputController.OnEquipInput += dataManager.inventory.EquipItem;
        inputController.OnAttackInput += itemCtrl.UseCurrentItem;
        inputController.OnReloadInput += itemCtrl.ReloadWeapon;
        inputController.OnDropInput += itemCtrl.Drop;
        #endregion

        playerMove.Init();        
    }
    private void OnDisable()
    {
        inputController.OnStateChangeEvent -= playerFSM.SetState;
        if (playerMove is ISetDirection direction)
        {
            inputController.OnMoveInput -= direction.SetDirection;
        }
        if (playerMove is IJump jump)
        {
            inputController.OnJumpInput -= jump.Jump;
        }
        inputController.OnLookInput -= cameraController.UpdateRotate;
        inputController.OnEquipInput -= dataManager.inventory.EquipItem;
        inputController.OnAttackInput -= itemCtrl.UseCurrentItem;
        inputController.OnReloadInput -= itemCtrl.ReloadWeapon;
        inputController.OnDropInput -= itemCtrl.Drop;
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
        playerSkill.UpdateSkillCtrl();
    }
}
