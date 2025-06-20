using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : DestroySingleton<WeaponManager>
{
    List<GameObject> weapons = new List<GameObject>();

    public void registWeapon(GameObject newWeapon)
    {
        weapons.Add(newWeapon);
    }
}
