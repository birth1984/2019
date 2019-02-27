using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

public class MyAssetHandler 
{
    [OnOpenAssetAttribute(1)]
    public static bool step1(int instanceID , int line)
    {
        string name = EditorUtility.InstanceIDToObject(instanceID).name;
        Debug.Log("Open Asset step：1("+name +")");
        return false;   // we did not handle the open
    }

    // step2有一个索引为2的属性，因此将在step1之后调用
    [OnOpenAssetAttribute(2)]
    public static bool step2(int instanceID , int line)
    {
        //Debug.Log("Open Asset step：2("+ instanceID +")");
        //return false; //我们没有处理开放
        string path = AssetDatabase.GetAssetPath(EditorUtility.InstanceIDToObject(instanceID));
        string name = Application.dataPath + "/" + path.Replace("Assets/", "");
        Debug.Log("path:" + path);
        Debug.Log("name:" + name);

        if(name.EndsWith(".xx"))
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "D:/program files (x86)/notepad++/notepad++.exe";
            startInfo.Arguments = name;
            process.StartInfo = startInfo;
            process.Start();
            return true;
        }

        return false;
    }

    static bool OpenFileAtLineExternal(string fileName , int line)
    {
#if UNITY_EDITOR_OSX
        string sublimePath = @"/Applications/Sublime Text.app/Contents/SharedSupport/bin/subl";
        if (File.Exists(sublimePath))
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = sublimePath;
            proc.StartInfo.Arguments = string.Format("{0}:{1}:0", fileName, line);
            proc.Start();
            return true;
        } 
        else
        {
            return UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(fileName, line);
        }
#else
        return UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(fileName, line);
#endif

    }
}
