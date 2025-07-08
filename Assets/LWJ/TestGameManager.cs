using UnityEngine;
using UnityEngine.AddressableAssets;

public class TestGameManager : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 1001; i < 1015; i++)
        {
            WeaponManager.Instance.CreateWeapon(i);
        }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Prefab();       
    }

    private async void Prefab()
    {
        var weaponPrefab = Addressables.LoadAssetAsync<GameObject>("Assets/LWJ/Prefab/SuicideWeapon.prefab");
        await weaponPrefab.Task;
        GameObject obj = weaponPrefab.Result;
        Instantiate(obj);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
