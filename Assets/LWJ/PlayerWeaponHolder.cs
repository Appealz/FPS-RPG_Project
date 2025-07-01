using UnityEngine;

public class PlayerWeaponHolder : MonoBehaviour
{
    Vector3 originalLocalPos;
    Vector3 recoilOffset;
    float recoilSpeed = 20f;
    float recoilReturnSpeed = 10f;

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

}
