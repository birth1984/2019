using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGUIUse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        //Event e = Event.current;
        //if (e != null)
        //{
        //    switch (e.type)
        //    {
        //        case EventType.MouseDown:
        //            e.Use();
        //            break;
        //    }
        //}
        GUILayout.TextField("输入文字:");

        if (GUILayout.Button("asdfasdf", GUILayout.Width(100)))
        {
            Debug.Log("Click");
        }
    }
}
