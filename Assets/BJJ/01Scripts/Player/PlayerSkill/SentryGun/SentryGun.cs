using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SentryGun : MonoBehaviour, ISkillObject, IPoolLabel, IUpdateObject
{
    private Pool ownerPool;
    private GameObject owner;
    private IEngineerSkill engineerSkill;
    private Transform mount;

    private StatManager statManager;
    private IUnitFSM fsm;
    private ISentryGunAI ai;
    private ISentryGunWriteableContext context;
    private ISentryGunWeapon weapon;

    public void Create(Pool onwerPool)
    {
        this.ownerPool = onwerPool;
        gameObject.SetActive(false);
        mount = MyUtility.GetChildrenTrans(transform, "mount");
    }

    public void InitSpawnObj(GameObject ownerObj, ClassSkillData data)
    {
        owner = ownerObj;
        if(data is IEngineerSkill skill)
        {
            engineerSkill = skill;
            statManager = new StatManager(gameObject, new Dictionary<StatType, StatValue> {
                {StatType.HP, new StatValue(engineerSkill.SentryGunMaxHP)},
                {StatType.AttackRange, new StatValue(10f) },
                {StatType.AttackDamage, new StatValue(engineerSkill.SentryGunAttackDamage) },
                {StatType.AttackSpeed, new StatValue(engineerSkill.SentryGunAttackSpeed) }
            });
        }

        if (TryGetComponent<ISentryGunWeapon>(out weapon))
        {
            weapon.InitWeapon(mount);
        }
        else
            Debug.Log("SentryGun.cs - InitSapwnObj() - ISentryGunWeapon Can't Reference");

        if (TryGetComponent<ISentryGunWriteableContext>(out context))
        {
            context.Init(owner,statManager);
        }
        else
            Debug.LogError("SentryGun.cs - InitSapwnObj() - Context Can't Reference");

        if (TryGetComponent<IUnitFSM>(out fsm))
        {
            fsm.ResistState(StateType.Idle, new IdleState());
            fsm.ResistState(StateType.Attack, new SentryGunAttackState(weapon));
        }
        else
            Debug.LogError("SentryGun.cs - InitSapwnObj() - IUnitFSM Can't Reference");

        if (TryGetComponent<ISentryGunAI>(out ai))
        {
            ai.Init(fsm);
        }
        else
            Debug.LogError("SentryGun.cs - InitSapwnObj() - ISentryGunAI Can't Reference");

        EventBus_SpawnObject.Publish(new UpdateSpawnObject(UpdateType.Regist, this));
    }

    public void ObjectUpdate()
    {
        ai.AIUpdate();
        weapon.WeaponUpdate();
    }

    public void ReturnToPool()
    {
        EventBus_SpawnObject.Publish(new UpdateSpawnObject(UpdateType.Unregist, this));
        ownerPool.ReturnToPool(gameObject);
    }
}
