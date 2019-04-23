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
    private MyToggleData m_groupData;

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
            m_groupData.lable = value;
            m_label.text = value;
        }
    }

    public MyToggleGroup Group { get => m_group; set => m_group = value; }
    public MyToggleData GroupData { get => m_groupData; set => m_groupData = value; }
    public Toggle Toggle { get => m_toggle; set => m_toggle = value; }


    // Start is called before the first frame update
    void Start()
    {      
        runInEditMode = true;
        m_label = GetComponentInChildren<Text>();
        LabelText = m_label.text;

        m_pos = m_normal.transform.position;
        m_posVisable = new Vector3(m_pos.x, m_pos.y + 10000, m_pos.z);
        Toggle.onValueChanged.AddListener(OnToggle);
        Toggle.isOn = m_groupData.click;
        IsOn(Toggle.isOn);
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
    private void IsOn(bool ison)
    {
        if (ison)
        {
            m_selected.transform.position = m_pos;
        }
        else
        {
            m_selected.transform.position = m_posVisable;
        }
    }

    public void FreshData()
    {
        Debug.Log("FreshSelect:" + m_groupData.lable + " " + m_groupData.click);
        Toggle.isOn = m_groupData.click;
        IsOn(Toggle.isOn);

        m_label.text = LabelText = m_groupData.lable;
    }

}
