using UnityEngine;

public class Rifle : MonoBehaviour, IRangeWeapon
{
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
        // todo : currentAmmo 탄창수만큼 리셋 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
