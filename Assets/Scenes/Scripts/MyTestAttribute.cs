using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTestAttribute : PropertyAttribute
{
    public int max;
    public int min;

    public MyTestAttribute(int a , int b)
    {
        max = a;
        min = b;
    }
}
