using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unity3D研究院编辑器之[System.Serializable]数组List对象添加默认值（二十七）
public class TestSerializable : MonoBehaviour
{
    public DataA[] data = new DataA[5];
    public List<DataA> data2 = new List<DataA>() { new DataA() };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class DataA
{
    public int a = 500;
    public bool b = true;
    public DataB subData;
}

[System.Serializable]
public class DataB
{
    public int c = 300;
    public bool d = false;
}
