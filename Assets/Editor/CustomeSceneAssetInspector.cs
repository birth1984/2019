using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(UnityEditor.SceneAsset))]
public class CustomeSceneAssetInspector : Editor
{
    class Data
    {
        public GameObject go;
        public bool fold = true;
        public Dictionary<Object, Editor> editors = new Dictionary<Object, Editor>();
        public Dictionary<Object, bool> foldouts = new Dictionary<Object, bool>();
    }


    List<CustomeSceneAssetInspector.Data> datas = new List<Data>();

    public override void OnInspectorGUI()
    {
        Event e = Event.current;

        GUI.enabled = true;
        //if(path.EndsWith(".unity"))
        {
            Draw();
            if (e.type == EventType.DragUpdated)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                e.Use();
            }
            if (e.type == EventType.DragPerform)
            {
                Object o1 = DragAndDrop.objectReferences[0];
                if (o1 is GameObject)
                {
                    datas.Add(new Data() { go = o1 as GameObject });
                }
            }
        }

        // Unity3D研究院编辑器之脚本的属性显示在自定义窗口下（十八）
        //base.OnInspectorGUI();

        string path = AssetDatabase.GetAssetPath(target);

        Debug.Log("CustomeSceneAssetInspector path:" + path);

        GUI.enabled = true;
        if (path.EndsWith(".unity"))
        {
            Debug.Log(".....................................unity");
            //EditorGUILayout.Button("我是场景");

            GUILayout.Button("我是场景");
        }
    }

    // Unity3D研究院编辑器之脚本的属性显示在自定义窗口下（十八）
    Vector2 scrollPos = Vector2.zero;
    void Draw()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        foreach (Data data in datas)
        {
            Editor editor = Editor.CreateEditor(data.go);
            data.fold = EditorGUILayout.InspectorTitlebar(data.fold, data.go);

            if (data.fold)
            {
                editor.OnInspectorGUI();
                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                GUILayout.BeginVertical();
                foreach (Component c in data.go.GetComponents<Component>())
                {
                    if (!data.editors.ContainsKey(c))
                        data.editors.Add(c, Editor.CreateEditor(c));
                }
                foreach (Component c in data.go.GetComponents<Component>())
                {
                    if (!data.editors.ContainsKey(c))
                    {
                        data.foldouts[c] = EditorGUILayout.InspectorTitlebar(data.foldouts.ContainsKey(c) ? data.foldouts[c] : true, c);
                        if (data.foldouts[c])
                            data.editors[c].OnInspectorGUI();
                    }
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndScrollView();
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
}