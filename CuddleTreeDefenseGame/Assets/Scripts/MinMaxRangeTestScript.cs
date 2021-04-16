using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Returns 2 values in a Vector2, a min and a max.

public class MinMaxRangeTestScript : MonoBehaviour
{
    //Chose a min value and a max value.
    //In the inspector cannot pick a lower value than min, or higher than max
    //Can be negative or positive values
    //Rounds down to 2 decimals

    //Must return as a vector2 or will return error.
    [MinMaxRange(-50f, 10f)]
    [SerializeField]
    Vector2 testRange;
    private void Start()
    {
        //Return min and max value from Vector2
        Debug.Log("Min value: " + testRange.x);
        Debug.Log("Max value: " + testRange.y);
    }

    //Can use properties to seperate the values:
    public float MinValue { get => testRange.x; set => testRange.x = value; }
    public float MaxValue { get => testRange.y; set => testRange.y = value; }
}
