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

    public void AttachWeapon(int weaponID)
    {
        if (equipWeaponObj == null)
            return;
        equipWeaponObj.SetActive(false);
        GameObject equipWeapon = WeaponManager.Instance.EquipWeapon(weaponID);
        equipWeapon.transform.SetParent(transform);
        equipWeapon.transform.localPosition = Vector3.zero;
        equipWeapon.transform.localRotation = Quaternion.identity;
        equipWeapon.SetActive(true);
        equipWeaponObj = equipWeapon;
    }

}
