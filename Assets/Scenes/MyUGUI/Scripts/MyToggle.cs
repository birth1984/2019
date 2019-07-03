using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[System.Serializable]
public class MyToggle : MonoBehaviour
{
    [SerializeField]
    private Toggle m_toggle;
    [SerializeField]
    private Image m_normal;
    [SerializeField]
    private Image m_selected;
    [SerializeField]
    private Text m_label;

    private MyToggleGroup m_group;
    [SerializeField]
    private MyToggleData m_myToggleData;

    public Vector3 m_pos;
    public Vector3 m_posVisable;


    public string LabelText
    {//lambda 表达式
        get
        {
            return m_label.text;
        }
        set
        {
            //m_myToggleData.lable = value;
            m_label.text = value;
        }
    }

    public MyToggleGroup Group { get => m_group; set => m_group = value; }
    public MyToggleData ToggleData { get => m_myToggleData; set => m_myToggleData = value; }
    public Toggle Toggle { get => m_toggle; set => m_toggle = value; }


    // Start is called before the first frame update
    void Start()
    {      
        //runInEditMode = true;      
        m_pos = m_normal.transform.position;
        m_posVisable = new Vector3(m_pos.x, m_pos.y + 10000, m_pos.z);
        Toggle.onValueChanged.AddListener(OnToggle);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnToggle(bool selected)
    {
        Debug.Log("Toggle value is " + selected.ToString());
        IsOn(selected);
    }
    public void IsOn(bool ison)
    {
        if (ison)
        {
            m_selected.transform.position = m_pos;
        }
        else
        {
            m_selected.transform.position = m_posVisable;
        }
        m_toggle.isOn = ison;
    }

    public void FreshData()
    {
        Debug.Log("FreshSelect:" + m_myToggleData.lable + " " + m_myToggleData.click);
        IsOn(m_myToggleData.click);
        m_label.text = LabelText = m_myToggleData.lable;
    }

}
