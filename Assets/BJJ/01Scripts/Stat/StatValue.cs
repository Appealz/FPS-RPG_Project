using System.Collections.Generic;
using UnityEngine;

public class StatValue
{
    private float baseValue;
    private List<float> addModifiers = new List<float>();
    private List<float> multiModifiers = new List<float>();

    public StatValue(float newValue)
    {
        baseValue = newValue;
    }

    public float Value
    {
        get
        {
            float total = baseValue;
            foreach (var modifier in addModifiers)
                total += modifier;
            float multiFactor = 1f;
            foreach (var modifier in multiModifiers)
            {
                multiFactor *= 1f + modifier;
            }
            total *= multiFactor;
            return total;
        }
    }

    public void SetBase(float newValue) => baseValue = newValue;

    public void AddModifier(float modifier, bool isMulti = false)
    {
        if(isMulti)
            multiModifiers.Add(modifier);
        else
            addModifiers.Add(modifier);
    }

    public void RemoveModifier(float modifier, bool isMulti = false)
    {
        if (isMulti)
            multiModifiers.Remove(modifier);
        else
            addModifiers.Remove(modifier);
    }

    public bool HasChange(float prev)
    {
        return Mathf.Approximately(Value, prev);
    }
}
