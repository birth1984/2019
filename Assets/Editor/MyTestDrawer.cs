using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MyTestAttribute))]
public class MyTestDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);
        MyTestAttribute attribute = (MyTestAttribute)base.attribute;

        property.intValue = Mathf.Min(Mathf.Max(EditorGUI.IntField(position, label.text, property.intValue), attribute.min), attribute.max);
    }
}
