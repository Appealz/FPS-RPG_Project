using UnityEngine;

public class ShotGun : MonoBehaviour, IRangeWeapon
{
    public bool useable => throw new System.NotImplementedException();

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
}
