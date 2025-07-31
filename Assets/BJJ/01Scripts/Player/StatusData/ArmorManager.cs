using UnityEngine;

public class EquipArmorData
{
    public bool isEquipArmor;
    public IItem curArmor; // todo 방탄복 인터페이스
    public float curDurability;

    public EquipArmorData(bool isEquipArmor, IItem curArmor, float curDurability)
    {
        this.isEquipArmor = isEquipArmor;
        this.curArmor = curArmor;
        this.curDurability = curDurability;
    }
}


public class ArmorManager
{
    // 방탄복 인터페이스 만들어지면 여기서 참조할 예정
    private bool isEquipArmor;
    private IItem curArmorData;
    private float curArmor;
    private float reduction; // 임시

    public ArmorManager()
    {
        isEquipArmor = false;
        curArmorData = null;
        curArmor = 0;
        reduction = 0;
    }

    /// <summary>
    /// 새로운 방탄복을 구매할 경우
    /// </summary>
    public void SetArmor(IItem newArmor) // todo 방탄복이 만들어지면 해당 방탄복을 받는 구조로 만들예정
    {
        curArmorData = newArmor;
        var data = curArmorData.GetItemCurrentData();
        curArmor = data.durability;
        // todo UI 업데이트
    }

    /// <summary>
    /// 방탄복을 수리할 경우
    /// </summary>
    public void RepairArmor()
    {
        // todo 방탄복이 만들어지면 curArmor를 현재 가지고 있는 방탄복의 최대 수치까지 수리할 예정
        curArmor = curArmorData.GetItemCurrentData().durability;
        // todo UI 업데이트
    }

    public int GetArmor() => Mathf.RoundToInt(curArmor);

    public float ApplyDamage(float damage)
    {
        float curDamage = damage * (1 - reduction);
        float absorb = curDamage - curArmor;
        curArmor -= curDamage;
        if(curArmor <= 0)
        {
            curArmor = 0;
            // todo 방탄복 UI 꺼버릴 예정
        }

        return Mathf.Max(0, absorb);
    }

    public EquipArmorData GetEquipArmorData()
    {
        return new EquipArmorData(isEquipArmor, curArmorData, curArmor);
    }
}
