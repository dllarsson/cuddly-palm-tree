using UnityEngine;
public class MinMaxRangeAttribute : PropertyAttribute
{
    public float minLimit = 0f;
    public float maxLimit = 100f;

    public MinMaxRangeAttribute(float min, float max)
    {
        minLimit = min;
        maxLimit = max;
    }
}