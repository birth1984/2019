﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyToggleGroup : MonoBehaviour
{
    [SerializeField]
    private List<MyToggleData> m_myToggleDatas = new List<MyToggleData>();
    // Start is called before the first frame update
    private Toggle[] m_toggles;

    public List<MyToggleData> MyToggleDatas { get => m_myToggleDatas; set => m_myToggleDatas = value; }

    void Start()
    {
        //m_group.allowSwitchOff = true;
        //m_toggles = GetComponentsInChildren<Toggle>();
        //Log("MyToggleDatas size " + MyToggleDatas.Count);
        //foreach (Toggle element in m_toggles)
        //{
        //    Debug.Log("My Toggle Group " + element.GetComponentInChildren<Text>().text + " " + element.name);
            //MyToggleData data = new MyToggleData();
            //data.myToggle.ToggleData = data;
            //element.isOn = data.click;
            //data.myToggle.Group = this;
            //data.lable = element.GetComponentInChildren<Text>().text;
            //data.myToggle = element.GetComponentInChildren<MyToggle>();
            
            
            //MyToggleDatas.Add(data);
            //Debug.Log("MyToggleDatas size " + MyToggleDatas.Count);
        //}

        foreach(MyToggleData data in m_myToggleDatas)
        {
            data.myToggle.ToggleData = data;
            data.myToggle.Toggle.isOn = data.click;
            data.myToggle.LabelText = data.lable; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class MyToggleData 
{
    public MyToggle myToggle;

    public string lable = "刘英";

    public bool click = false;
}
