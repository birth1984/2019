using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml;
using System.IO;

[CustomEditor(typeof(EditorPlayer))]
public class KikiEditorExtension : Editor
{
    private string m_rhythm ;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorPlayer editorPlayer = (EditorPlayer)target;
        EditorUIManager.GetInstance().BPM = int.Parse( EditorGUILayout.TextField("BPM:" , EditorUIManager.GetInstance().BPM.ToString()) );
        RhythmManager.Instance().NewRhythmID = int.Parse(EditorGUILayout.TextField("ID:" , (RhythmManager.Instance().RhythmNodeList.Count + 1).ToString())) ;
        RhythmManager.Instance().NewRhythmName = EditorGUILayout.TextField( "节奏名称" , RhythmManager.Instance().NewRhythmName );

        
        if (GUILayout.Button("Add New Rhythm"))
        {
            RhythmManager.Instance().InsertOneRhythm();
        }

        //GUILayout.Label("Basic Rhythm List");
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("<<<<<<< Basic Rhythm List >>>>>>>");
        EditorGUILayout.LabelField("");


        foreach (XmlElement beatXml in RhythmManager.Instance().RhythmNodeList)
        {
            EditorGUILayout.ToggleLeft(beatXml.GetAttribute("Name"), false);            
        }                

        if (GUILayout.Button("Fresh Rhythm List"))
        {
            
        }
    }

    

    private void CreateXML()
    {
        string path = Application.dataPath + "/Atlas/Config/KikiBasicRhythm3.xml";
        if(!File.Exists(path))
        {
            XmlDocument xml = new XmlDocument();
            XmlDeclaration dec = xml.CreateXmlDeclaration("1.0", "utf-8", "yes");
            
            XmlElement root = xml.CreateElement("Config");
            //XmlElement element = xml.CreateElement("Rhythm");
            //element.SetAttribute("Id", "1");
            //element.SetAttribute("Name", "Basic Rhythm");
            //for(int i = 0; i < 4; i++)
            //{
            //    XmlElement elementChild = xml.CreateElement("Beat");
            //    elementChild.SetAttribute("Bar", i+"");
            //    elementChild.SetAttribute("Pos", "0");
            //    elementChild.SetAttribute("SoundType", "1");
            //    elementChild.SetAttribute("Ya", "0");
            //    element.AppendChild(elementChild);
            //}
            //root.AppendChild(element);
            xml.AppendChild(root);
            xml.Save(path);
        }
    }
}
