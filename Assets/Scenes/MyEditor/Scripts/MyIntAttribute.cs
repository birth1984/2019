using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyIntAttribute : PropertyAttribute
{
    public int intMax;
    public int intMin;

    public MyIntAttribute(int a , int b)
    {
        intMax = a;
        intMin = b;
    }

    
}
