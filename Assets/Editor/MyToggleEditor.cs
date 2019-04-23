using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyToggle))]
// 在编辑模式下执行脚本
[ExecuteInEditMode]
public class MyToggleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        MyToggle toggle = (MyToggle)target; 
        
        toggle.LabelText = EditorGUILayout.TextField("文本显示", toggle.LabelText);
        toggle.ToggleData.lable = toggle.LabelText;
        
        toggle.Toggle.isOn = EditorGUILayout.Toggle("Selectde", toggle.Toggle.isOn);
        //toggle.ToggleData.click = toggle.Toggle.isOn;
        toggle.IsOn(toggle.Toggle.isOn);
    }
}


