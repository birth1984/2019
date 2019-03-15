#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Unity3D研究院编辑器之EditorOnly（二十八）
//https://www.xuanyusong.com/archives/4384
public class EditorOnly : MonoBehaviour
{
    [HideInInspector]
    public string tag = "Untagged" ;

    private void OnDrawGizmos()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag(tag))
        {
            UnityEditor.Handles.Label(go.transform.position, tag);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[CustomEditor(typeof(EditorOnly))]
public class EditorOnlyEditor:Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorOnly gizmos = target as EditorOnly;
        EditorGUI.BeginChangeCheck();
        gizmos.tag = EditorGUILayout.TagField("Tag for Object:", gizmos.tag);
        if(EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(gizmos);
        }
    }
}
#endif

























