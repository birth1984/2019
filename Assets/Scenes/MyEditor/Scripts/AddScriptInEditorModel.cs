using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScriptInEditorModel : MonoBehaviour
{
    public string Name; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#if UNITY_EDITOR
    private void Reset()
    {
        Debug.Log("脚本添加事件");
    }

    private void OnValidate()
    {
        Debug.Log("脚本对象数据发生改变事件");
    }
#endif
}
