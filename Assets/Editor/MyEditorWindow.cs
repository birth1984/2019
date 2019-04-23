using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using UnityEditor.ProjectWindowCallback;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using UnityEngine.Profiling;

public class MyEditorWindow : EditorWindow
{
    // 打开一个500X500的窗口 Unity3D研究院之拓展自定义编辑器窗口（二）
    [MenuItem("BirthEditor/Window500x500")]
    static void AddWindow()
    {
        Rect wr = new Rect(0, 0, 500, 500);

        MyEditorWindow window = (MyEditorWindow)EditorWindow.GetWindowWithRect(typeof(MyEditorWindow), wr, true, "window 500 X 500");

        window.Show();
    }

    // 打开一个可以拉伸的窗口 Unity3D研究院之拓展自定义编辑器窗口（二）
    [MenuItem("BirthEditor/WindowFreeSize")]
    static void AddWindow1()
    {
        MyEditorWindow window = (MyEditorWindow)EditorWindow.GetWindow(typeof(MyEditorWindow), true, "window free size");

        window.Show();
    }

    // 删除一个模型上的全部子对象的Collider 并且重新计算父模型的包围盒 可以包住全部子对象 雨松（六）
    [MenuItem("BirthEditor/Do BoxCollider Test")]
    static void TestBoxCollider()
    {
        Transform parent = Selection.activeGameObject.transform;
        Vector3 postion = parent.position;
        Quaternion rotation = parent.rotation;
        Vector3 scale = parent.localScale;
        parent.position = Vector3.zero;
        parent.rotation = Quaternion.Euler(Vector3.zero);
        parent.localScale = Vector3.one;

        Collider[] colliders = parent.GetComponentsInChildren<Collider>();
        foreach (Collider child in colliders)
        {
            DestroyImmediate(child);
        }

        Vector3 center = Vector3.zero;
        Renderer[] renders = parent.GetComponentsInChildren<Renderer>();
        foreach( Renderer child in renders)
        {
            center += child.bounds.center;
        }
        center /= renders.Length;
        // 包围盒
        Bounds bounds = new Bounds(center, Vector3.zero);

        foreach(Renderer child in renders)
        {
            // 扩大包围盒
            bounds.Encapsulate(child.bounds);
        }

        BoxCollider boxCollider = parent.gameObject.AddComponent<BoxCollider>();
        boxCollider.center = bounds.center - parent.position;
        boxCollider.size = bounds.size;

        parent.position = postion;
        parent.rotation = rotation;
        parent.localScale = scale;

    }

    // 自动计算包围盒的中心 雨松（七）
    [MenuItem("BirthEditor/Do Model 2 Center")]
    static void DoModel2Center()
    {
        
        Transform parent = Selection.activeGameObject.transform;
        Vector3 position = parent.position;
        Quaternion rotation = parent.rotation;
        Vector3 scale = parent.localScale;

        parent.position = Vector3.zero;
        parent.rotation = Quaternion.Euler(Vector3.zero);
        parent.localScale = Vector3.one;


        Vector3 center = Vector3.zero;
        Renderer[] renders = parent.GetComponentsInChildren<Renderer>();
        foreach(Renderer child in renders)
        {
            center += child.bounds.center;
        }
        center /= renders.Length;

        Bounds bounds = new Bounds(center, Vector3.zero);
        foreach(Renderer child in renders)
        {
            //Debug.Log(">>>>>>>>>>>>>>Child Bounds " + child.bounds.ToString());
            bounds.Encapsulate(child.bounds);
            //Debug.Log(">>>>>>>>>>>>>>Bounds " + bounds.ToString());
        }

        parent.position = position;
        parent.rotation = rotation;
        parent.localScale = scale;
        //Debug.Log(">>>>>>>>>>>>>>A" + parent.position.ToString() );
        //Debug.Log(">>>>>>>>>>>>>>T" + parent.transform.position.ToString());
        foreach (Transform t in parent)
        {
            t.position = t.position - bounds.center;
        }
        //Debug.Log(">>>>>>>>>>>>>>B" + bounds.center.ToString());
        parent.transform.position = bounds.center + parent.position;
    }

    public class ColorData
    {
        public string name;
        public Color color;
    }
    // Unity3D研究院编辑器之脚本生成Preset Libraries（十四）
    [MenuItem("BirthEditor/Creat Color Tool")]
    static void CreatColorTool()
    {
        // 复制一份新模板到new ColorPath 目录下
        string templateColorPath = "Assets/Template/color.colors";
        string newColorPath = "Assets/Editor/界面A.colors";
        AssetDatabase.DeleteAsset(newColorPath);
        AssetDatabase.CopyAsset(templateColorPath, newColorPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        //return;
        // 这里我写两条临时数据
        List<ColorData> ColorList = new List<ColorData>()
        {
            new ColorData(){ name = "按钮样式1颜色" , color = Color.green } ,
            new ColorData(){ name = "按钮样式2颜色" , color = new Color(0.1f,0.1f,0.1f,0.1f) }
        };

        UnityEngine.Object newColor = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(newColorPath);
        SerializedObject serializedObject = new SerializedObject(newColor);
        SerializedProperty property = serializedObject.FindProperty("m_Presets");
        property.ClearArray();


        // 把测试数据写进去
        for(int i = 0; i < ColorList.Count; i++)
        {
            property.InsertArrayElementAtIndex(i);
            SerializedProperty colorsProperty = property.GetArrayElementAtIndex(i);
            colorsProperty.FindPropertyRelative("m_Name").stringValue = ColorList[i].name;
            colorsProperty.FindPropertyRelative("m_Color").colorValue = ColorList[i].color;
        }

        serializedObject.ApplyModifiedProperties();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    // Unity3D研究院编辑器之不实例化Prefab获取删除更新组件（十五）
    [MenuItem("BirthEditor/Delete")]
    static void DeletePrefabComponent()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GameObject.prefab");

        //删除MeshCollider
        MeshCollider[] meshColliders = prefab.GetComponentsInChildren<MeshCollider>(true);
        foreach(MeshCollider meshCollider in meshColliders)
        {
            GameObject.DestroyImmediate(meshCollider, true);
        }

        // 删除Animation组件
        Animation[] animations = prefab.GetComponentsInChildren<Animation>(true);
        foreach (Animation ani in animations)
        {
            if(ani.clip == null)
            {
                GameObject.DestroyImmediate(ani, true);
            }           
        }

        // 删除Missing的脚本
        MonoBehaviour[] monoBehaviours = prefab.GetComponentsInChildren<MonoBehaviour>(true);
        foreach(MonoBehaviour script in monoBehaviours)
        { 
            if(monoBehaviours == null)
            {
                Debug.Log("有个missing的脚本");
                // 删除脚本失败 并没什么卵用
                //GameObject.DestroyImmediate(monoBehaviour,true);
            }
        }

        // 遍历Transform的名字，并且给某个对象添加脚本
        Transform[] transforms = prefab.GetComponentsInChildren<Transform>(true);
        foreach(Transform trans in transforms)
        {
            if(trans.name == "GameObject(2)")
            {
                Debug.Log("GameObject(2)" + trans.parent.name);
                trans.gameObject.AddComponent<BoxCollider>();
                return;
            }
        }

        // 遍历Transform的名字，删除某个GameObject节点
        foreach(Transform trans in transforms)
        {
            if(trans.name == "GameObject(2)")
            {
                GameObject.DestroyImmediate(trans.gameObject, true);
                return;
            }
        }
        // 这个函数告诉引擎，相关对象所属于的Prefab已经发生了更改。方便，当我们更改了自定义对象的属性的时候，自动更新到所属的Prefab中。
        EditorUtility.SetDirty(prefab);
    }
    // 删除Missing脚本
    [MenuItem("BirthEditor/Cleanup Missing Scripts")]
    static void CleanupMissingScripts()
    {
        foreach(GameObject gameObj in Selection.gameObjects)
        {
            Component[] components = gameObj.GetComponents<Component>();
            SerializedObject serializedObject = new SerializedObject(gameObj);

            SerializedProperty prop = serializedObject.FindProperty("m_Component");

            int r = 0;
            for (int i = 0; i < components.Length; i++)
            {
                Component comp = components[i];
                if (comp == null)
                {
                    prop.DeleteArrayElementAtIndex(i - r);
                    r++;
                }
            }

            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(gameObj);
        }
    }

    //Unity3D研究院编辑器之创建Lua脚本模板（十六）
    [MenuItem("BirthEditor/CreateLuaScript" , false , 80)]
    public static void CreateNewLua()
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists
            (
                0,
                ScriptableObject.CreateInstance<MyDoCreateScriptAsset>(),
                GetSelectedPathOrFallback() + "/New Lua.lua",
                null ,
                "Assets/Editor/Lua/Templete/Lua.lua"
            );
        
    }

    public static string GetSelectedPathOrFallback()
    {
        string path = "Assets";
        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets)) 
        {
            path = AssetDatabase.GetAssetPath(obj);
            if(!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }        
        return path;
    }
    
    class MyDoCreateScriptAsset: EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            //throw new NotImplementedException();
            UnityEngine.Object o = CreateScritpAssetFromTemplate(pathName, resourceFile);
            ProjectWindowUtil.ShowCreatedAsset(o);
        }

        internal static UnityEngine.Object CreateScritpAssetFromTemplate(string pathName , string resourceFile)
        {
            string fullPath = Path.GetFullPath(pathName);
            StreamReader streamReader = new StreamReader(resourceFile);
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName);
            text = Regex.Replace(text, "#NAME#", fileNameWithoutExtension);

            bool encoderShouldEmitUTF8Identifier = true;
            bool throwOnInvalidBytes = false;
            UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);
            bool append = false;
            StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding);
            streamWriter.Write(text);
            streamWriter.Close();
            AssetDatabase.ImportAsset(pathName);
            return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
        }
    }

    [MenuItem("HierarchyMenu/Eason")]
    static void HierarchyEason()
    {

    }

    [MenuItem("HierarchyMenu/Yoyo")]
    static void HierarchyYoyo()
    {

    }

    [MenuItem("HierarchyMenu/Birth")]
    static void HierarchyBirth()
    {

    }
    //Unity3D研究院编辑器之重写Hierarchy的右键菜单（二十二）
    [InitializeOnLoadMethod]
    static void StartInitializeOnLoadMethod()
    {
        //EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
    }

    static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        if (Event.current != null &&
            selectionRect.Contains(Event.current.mousePosition) &&
            Event.current.button == 1 &&
            Event.current.type <= EventType.MouseUp)
        {
            GameObject selectedGameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (selectedGameObject)
            {
                Vector2 mousePosition = Event.current.mousePosition;

                EditorUtility.DisplayPopupMenu(new Rect(mousePosition.x, mousePosition.y, 0, 0), "HierarchyMenu", null);
                Event.current.Use();
            }
        }
    }

    [MenuItem("BirthEditor/TextureMemoryViewer")]
    public static void MemoryViewer()
    {
        Texture target = Selection.activeObject as Texture;

        Type type = Assembly.Load("UnityEditor.dll").GetType("UnityEditor.TextureUtil");

        MethodInfo methodInfo = type.GetMethod("GetStorageMemorySize", BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);

        Debug.Log("内存占用：" + EditorUtility.FormatBytes(Profiler.GetRuntimeMemorySize(Selection.activeObject)));
        Debug.Log("硬盘占用：" + EditorUtility.FormatBytes((int)methodInfo.Invoke(null, new object[] { target })));
    }

    //UGUI研究院之全面理解图集与使用（三）
    [MenuItem("BirthEditor/Atlas Maker/Make Sprites")]
    static void MakeSprites()
    {
        string spriteDir = Application.dataPath + "/Resources/Prefabs/Sprites";
        
        if(!Directory.Exists(spriteDir))
        {
            Directory.CreateDirectory(spriteDir);
            Debug.Log("create spriteDir:" + spriteDir);
        }
        
        DirectoryInfo rootDirInfo = new DirectoryInfo(Application.dataPath + "/Atlas");

        List<string> strs = new List<string>();
        string tempDir;

        foreach (FileInfo pngFile in rootDirInfo.GetFiles("*.png", SearchOption.AllDirectories))
        {
            tempDir = spriteDir; ;
            string allPath = pngFile.FullName;
            string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
            Sprite spt = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Sprite)) as Sprite;
            if (spt == null)
            {
                Debug.Log("bug!!!" + assetPath );
                continue;
            }
                
            GameObject go = new GameObject(spt.name);
            go.AddComponent<SpriteRenderer>().sprite = spt;
            FilePathSubString(assetPath, strs);
            for (int i = 2; i < strs.Count; i++)
            {
                tempDir += "/" + strs[i];
            }
            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            allPath = tempDir + "/" + spt.name + ".prefab";
            string prefabPath = allPath.Substring(allPath.IndexOf("Assets"));
            strs.Clear();
            PrefabUtility.CreatePrefab(prefabPath, go);
            GameObject.DestroyImmediate(go);

            Debug.Log("+++++++++++++++++ Make Sprites End 100% ++++++++++++++++++");
        }
    } 

    static void FilePathSubString(string filePath , List<string> arr)
    {
        int index = filePath.IndexOf("\\");
        if (index > 0)
        {
            arr.Add( filePath.Substring(0, index) ) ;            
            filePath = filePath.Substring(index + 1);
            FilePathSubString(filePath, arr);
        }
        //else
        //{
        //    arr.Add(filePath);
        //}
    }

    
    [MenuItem("BirthEditor/Atlas Maker/Clean Sprites")]
    static void SpriteCleaner()
    {
        string spriteDir = Application.dataPath + "/Resources/Prefabs/Sprites";
        

        if (!Directory.Exists(spriteDir))
            return;

        DirectoryInfo rootDirInfo = new DirectoryInfo(spriteDir);

        RemoveAllFiles(rootDirInfo);

        Debug.Log("+++++++++++++++++ Clean Sprites End 100% ++++++++++++++++++");
    }

    static void RemoveAllFiles(DirectoryInfo rootDirInfo)
    {   
        if(rootDirInfo.GetDirectories().Length > 0)
        {
            foreach (DirectoryInfo rootDir in rootDirInfo.GetDirectories())
            {
                //Debug.Log(rootDir.Name + " : " + rootDir.FullName);
                RemoveAllFiles(rootDir);
                rootDir.Delete();
            }
        }       
        foreach (FileInfo file in rootDirInfo.GetFiles())
        {
            //Debug.Log(file.Name + " : " + file.FullName);
            file.Delete();
        }
    }


    private string text;
    private string prefabContext;

    private Texture texture;

    public void Awake()
    {
        //把prefab转换成文本输出
        prefabContext = File.ReadAllText(@"E:/Code/Unity\2019/Assets/Resources/Prefabs/Game.prefab");
    }

    private void OnGUI()
    {
        text = EditorGUILayout.TextField("输入文字:", text);

        if (GUILayout.Button("打开通知", GUILayout.Width(200)))
        {
            this.ShowNotification(new GUIContent("This is a Notification/r/n/p" + prefabContext ));  
        }

        if(GUILayout.Button("关闭通知" , GUILayout.Width(200)))
        {
            this.RemoveNotification();
        }

        EditorGUILayout.LabelField("鼠标在窗口位置", Event.current.mousePosition.ToString());

        texture = EditorGUILayout.ObjectField("添加贴图", texture, typeof(Texture), true) as Texture;

        if(GUILayout.Button("关闭窗口", GUILayout.Width(200)))
        {
            this.Close();
        }
        GUILayout.TextArea(prefabContext, GUILayout.Width(400));

    }

    private void Update()
    {
        
    }

    private void OnFocus()
    {
        Debug.Log("当窗口获得焦点时调用一次");
    }

    private void OnLostFocus()
    {
        Debug.Log("当窗口丢失焦点时调用一次");
    }

    private void OnHierarchyChange()
    {
        Debug.Log("当Hierarchy视图中的任何对象发生改变时调用一次");
    }

    private void OnProjectChange()
    {
        Debug.Log("当Project视图中的资源发生改变时调用一次");
    }

    private void OnInspectorUpdate()
    {
        //Debug.Log("窗口面板的更新");
        // 这里窗口会重绘，不然窗口信息不刷新
        this.Repaint();
    }

    private void OnSelectionChange()
    {
        //当窗口出去开启状态，并且在Hierarchy视图中选择某游戏对象时调用
        foreach (Transform t in Selection.transforms)
        {
            Debug.Log("OnSelectionChange : " + t.name);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("当窗口关闭时调用");
    }
}
