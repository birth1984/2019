using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyToggleGroup))]
// 在编辑模式下执行脚本，这里用处不大可以删掉
[ExecuteInEditMode]
public class MyToggleGroupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MyToggleGroup toggleGroup = (MyToggleGroup)target;
        //foreach (MyToggleData data in toggleGroup.MyToggleDatas)
        //{
        //    data.myToggle.FreshData();
        //}

        if (GUILayout.Button("保存"))
        {
            foreach (MyToggleData data in toggleGroup.MyToggleDatas)
            {
                Debug.Log("Lable:" + data.lable + " Select:" + data.click);
                data.myToggle.FreshData();
            }
        }


    }
}
