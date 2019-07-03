using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

[CustomEditor(typeof(ImageDoScale))]
public class MyDoTween : Editor
{
    public Ease m_myEase = Ease.Flash;
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ImageDoScale script = target as ImageDoScale;

        EditorGUILayout.BeginHorizontal();

        script.MyEase = (Ease)EditorGUILayout.EnumPopup("Ease:", script.MyEase);

        EditorGUILayout.EndHorizontal();


        if (GUILayout.Button("Test Ease"))
        {
            script.DoScale();
        }
    }

}
