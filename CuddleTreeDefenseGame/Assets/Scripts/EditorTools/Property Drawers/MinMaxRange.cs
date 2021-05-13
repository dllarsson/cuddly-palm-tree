using UnityEngine;
public class MinMaxRangeAttribute : PropertyAttribute
{
    public float minLimit;
    public float maxLimit;
    public int decimals;

    public MinMaxRangeAttribute(float min, float max, int decimals = 2)
    {
        minLimit = min;
        maxLimit = max;
        this.decimals = decimals;
    }
}