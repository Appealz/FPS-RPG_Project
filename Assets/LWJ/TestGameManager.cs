using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TestGameManager : MonoBehaviour
{
    Player player;

    private async void Awake()
    {
        //for (int i = 1001; i < 1015; i++)
        //{
        //    WeaponManager.Instance.CreateWeapon(i);
        //}
        await WeaponManager.Instance.CreateItemData();
        player = FindAnyObjectByType<Player>();
        player.Init();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Prefab();       
    }

    //private async void Prefab()
    //{
    //    var weaponPrefab = Addressables.LoadAssetAsync<GameObject>("Assets/LWJ/Prefab/SuicideWeapon.prefab");
    //    await weaponPrefab.Task;
    //    GameObject obj = weaponPrefab.Result;
    //    Instantiate(obj);
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
}
