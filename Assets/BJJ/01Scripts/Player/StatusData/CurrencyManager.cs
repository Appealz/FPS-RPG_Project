using UnityEngine;

public class CurrencyManager
{
    private int gold;

    public CurrencyManager()
    {
        gold = 0;
    }

    public void AddGold(int inputGold) => gold += inputGold;
    public int GetGold() => gold;

    public int RemoveGold(int outputGold) => gold -= outputGold;
}
