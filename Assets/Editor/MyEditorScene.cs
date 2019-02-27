using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Test1))]
public class MyEditorScene : Editor
{
    private void OnSceneGUI()
    {
        Test1 test = (Test1)target;

        Handles.Label(test.transform.position + Vector3.up * 2, test.transform.name + " : " + test.transform.position.ToString());
        

        Handles.BeginGUI();

        GUILayout.BeginArea(new Rect(100, 100, 100, 100));

        if(GUILayout.Button("this was a button !"))
        {
            Debug.Log("test");
        }

        GUILayout.Label("TP:" + test.transform.position.ToString());
        GUILayout.Label("我在编辑Scene视图");

        GUILayout.EndArea();

        Handles.EndGUI();
    }
}
