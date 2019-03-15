using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDepth : MonoBehaviour
{
    public int order;
    public bool isUI = true;

    // Start is called before the first frame update
    void Start()
    {
        if(isUI)
        {
            Canvas canvas = GetComponent<Canvas>();
            if(canvas == null )
            {
                canvas = gameObject.AddComponent<Canvas>();
            }
            canvas.overrideSorting = true;
            canvas.sortingOrder = order;
        }
        else
        {
            Renderer[] renders = GetComponentsInChildren<Renderer>();
            foreach (Renderer render in renders )
            {
                render.sortingOrder = order;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
