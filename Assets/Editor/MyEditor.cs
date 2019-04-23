using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

// 自定义Test脚本
[CustomEditor(typeof(Test))]
// 在编辑模式下执行脚本，这里用处不大可以删掉
[ExecuteInEditMode]
// 请继承Editor
public class MyEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        //得到Test对象
        Test test = (Test)target;

        test.MRectValue = EditorGUILayout.RectField("窗口坐标", test.MRectValue);

        test.Texture = EditorGUILayout.ObjectField("增加一张贴图", test.Texture, typeof(Texture), true) as Texture;

    }

    [InitializeOnLoadMethod]
    static void Start()
    {
        Action OnEvent = delegate
        {
            Event e = Event.current;

            switch (e.type)
            {
                //case EventType.DragPerform:
                //    Debug.Log("DragPerform");
                //    e.Use();
                //    break;
                //case EventType.DragUpdated:
                //    Debug.Log("DragUpdated");
                //    e.Use();
                //    break;
                //case EventType.DragExited:
                //    Debug.Log("DragExited");
                //    e.Use();
                //    break;
            }
        };

        //EditorApplication.hierarchyWindowItemOnGUI = delegate (int instanceID, Rect setlectionRect)
        //{
        //    OnEvent();
        //};
    }

   
    
}
