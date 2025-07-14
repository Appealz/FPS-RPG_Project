using UnityEngine;

public class RifleSkillLogic : ClassSkill, IAnimSkill
{
    private IRifleSkill rifleSkillData;

    public RifleSkillLogic(GameObject newOwner, ClassSkillData data) : base(newOwner, data)
    {
        rifleSkillData = GetInterface<IRifleSkill>();
    }

    public void OnSkillAction()
    {
        Debug.Log("Test Code - Rifle Motion");

        GameObject grenade = PoolManager.Instance.GetPool("Rifle_Grenade").GetObjFromPool();
        if(grenade.TryGetComponent<ISkillObject>(out var so))
        {
            so.InitSpawnObj(owner, skillData);
        }
        if(grenade.TryGetComponent<Rigidbody>(out Rigidbody grd))
        {
            // todo 화면 정중앙 (크로스헤어 기준)으로 수정할 필요가 있음
            grd.AddForce(owner.transform.forward * 15, ForceMode.Impulse);
        }
    }

    public override void UseSkill()
    {
        if (!IsUseable)
        {
            Debug.Log($"작동 불가 - 쿨타임 {curCoolDown}");
            return;
        }

        curCoolDown = skillData.CoolDown;
        StartCooldown();
        // 추후 애니메이션 이벤트에 기반하여 스킬 작동 예정
        Debug.Log("Test Code - Rifle");
    }
}
