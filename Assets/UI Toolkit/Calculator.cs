using UnityEngine;

public class Calculator
{
    public float Plus(float a, float b)
    {
        return a + b;
    }

    public float Minus(float a, float b)
    {
        return a - b;
    }

    public float Multiply(float a, float b)
    {
        return a * b;
    }

    public float Divide(float a, float b)
    {
        if (b == 0f)
            return float.NaN;

        return a / b;
    }

}
