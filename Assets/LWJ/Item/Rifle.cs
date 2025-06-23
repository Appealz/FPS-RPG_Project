using UnityEngine;

public class Rifle : MonoBehaviour, IRangeWeapon
{
    // 아이템자체가 발동가능한지 여부.
    public bool useable => currentAmmo > 0 || !isAttacking;
    private bool isAttacking;
    private int currentAmmo;
    private int currentMagazine;

    // todo : 아이템 데이터 주입
    // private ItemData itemData
    public void InitData()
    {
        
    }

    public void Use() => Fire();

    private void Fire()
    {        
        if(!useable)
            return;
        isAttacking = true;
        currentAmmo--;
        // todo : 1) 유니태스크로 attackRate에 따라 isAttacking 관리.
        //        2) 총알 및 레이케스트 발사.
    }

    public void Reload()
    {        
        if (currentMagazine == 0)
            return;
        currentMagazine--;
        
        // 애니메이션 이벤트를 이벤트버스로
    }

    // todo : currentAmmo 탄창수만큼 리셋 






}
