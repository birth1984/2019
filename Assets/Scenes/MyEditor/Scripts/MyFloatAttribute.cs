using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFloatAttribute : PropertyAttribute
{
    public float floatMax;
    public float floatMin;

    public MyFloatAttribute(float max, float min)
    {
        floatMax = max;
        floatMin = min;
    }
}
