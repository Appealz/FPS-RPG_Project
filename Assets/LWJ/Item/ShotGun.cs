using UnityEngine;

public class ShotGun : MonoBehaviour, IRangeWeapon
{
    public bool useable => currentAmmo > 0 && !isAttacking;

    public AnimationClip useClip => null;

    public AnimationClip reloadClip => throw new System.NotImplementedException();

    private bool isAttacking;
    private int currentAmmo;
    private int currentMagazine;

    private float damage;
    private float attackRate;
    private WeaponData myData;

    // todo : 아이템 데이터 주입
    // private ItemData itemData    
    public void InitData()
    {

    }

    public void Use() => Fire();

    private void Fire()
    {

    }

    public void Reload()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitWeaponData(WeaponData newData)
    {
        throw new System.NotImplementedException();
    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }
}
