using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPropertyAttributeDrawer : MonoBehaviour
{
    [MyTestAttribute(100,0)]
    public int intvalue = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
