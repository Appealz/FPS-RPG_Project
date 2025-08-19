using System;
using UnityEngine;

public class CalculateViewModel
{
    public Calculator model;

    public float amount_1;
    public float amount_2;
    public float result;

    public event Action<float> OnResultChanged;

    public CalculateViewModel(Calculator newModel)
    {
        model = newModel;
    }

    public void DoPlus()
    {
        result = model.Plus(amount_1, amount_2);
        Notify();
    }

    public void DoMinus()
    {
        result = model.Minus(amount_1, amount_2);
        Notify();
    }

    public void DoMultiply()
    {
        result = model.Multiply(amount_1, amount_2);
        Notify();
    }
    public void DoDivide()
    {
        result = model.Divide(amount_1, amount_2);
        Notify();
    }

    private void Notify()
    {
        Action<float> notify = OnResultChanged;
        if(notify != null)
        {
            notify.Invoke(result);
        }
    }

}

