using UnityEngine;

public class PlayerWeaponHolder : MonoBehaviour
{
    Vector3 originalLocalPos;
    Vector3 recoilOffset;
    float recoilSpeed = 20f;
    float recoilReturnSpeed = 10f;

    GameObject equipWeaponObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalLocalPos = transform.localPosition;
    }

    public void WeaponRecoil(float weaponRecoil)
    {
        recoilOffset += Vector3.back * weaponRecoil;
    }

    private void LateUpdate()   
    {
        recoilOffset = Vector3.Lerp(recoilOffset, Vector3.zero, Time.deltaTime * recoilReturnSpeed);
        transform.localPosition = Vector3.Lerp(originalLocalPos, originalLocalPos + recoilOffset, Time.deltaTime * recoilSpeed);
    }

    public void WeaponSetting(IItem newItem)
    {
        // todo : 오브젝트 풀 객체 가져옴
        GameObject obj;
        //obj.TryGetComponent<IItem>()
    }

    public void AttachWeapon(int weaponID)
    {        
        if (equipWeaponObj != null)
        {
            equipWeaponObj.SetActive(false);
        }
        
        GameObject equipWeapon = WeaponManager.Instance.EquipWeapon(weaponID);

        equipWeapon.SetActive(true);
        equipWeapon.transform.SetParent(transform, false);
        equipWeapon.transform.localPosition = Vector3.zero;
        equipWeapon.transform.localRotation = Quaternion.identity;
        equipWeapon.transform.localScale = Vector3.one; // 추가했는지 확인
        
        equipWeaponObj = equipWeapon;
    }

}
