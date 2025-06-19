using UnityEngine;

public class CurrencyManager
{
    private GameObject owner;
    private int gold;

    public CurrencyManager(GameObject newOwner)
    {
        gold = 0;
        owner = newOwner;
    }

    public void AddGold(int inputGold) => gold += inputGold;
    public int GetGold() => gold;

    public int RemoveGold(int outputGold) => gold -= outputGold;
}
