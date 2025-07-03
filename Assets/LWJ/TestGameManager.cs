using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 1001; i < 1011; i++)
        {
            WeaponManager.Instance.CreateWeapon(i);
        }
        
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
