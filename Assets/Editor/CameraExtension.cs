using UnityEngine;
using UnityEditor;

public class CameraExtension : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        if(GUILayout.Button("蔡牡丹"))
        {

        }
    }
}

[CanEditMultipleObjects()]
[CustomEditor(typeof(Camera) , true)]
public class CustomExtension : CameraExtension
{

}
