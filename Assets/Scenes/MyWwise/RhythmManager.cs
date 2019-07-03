using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;


public class RhythmManager 
{
    private static RhythmManager m_instance = null;

    public static RhythmManager Instance()
    {
        if(m_instance == null)
        {
            m_instance = new RhythmManager();
        }
        return m_instance;
    }

    RhythmManager()
    {
        CheckXMLExist();
    }

    //基本节奏配置表
    private XmlDocument m_rhythmXml;
    private XmlDeclaration m_dec;
    //private XmlNode m_configRoot;
    private XmlNodeList m_rhythmNodeList;
    public XmlNodeList RhythmNodeList { get => m_rhythmNodeList; set => m_rhythmNodeList = value; }
    

    private string m_xmlPath;

    private int m_newRhythmID = -1;
    public int NewRhythmID { get => m_newRhythmID; set => m_newRhythmID = value; }
    private string m_newRhythmName = "Basic Rhythm";
    public string NewRhythmName { get => m_newRhythmName; set => m_newRhythmName = value; }
    

    public void CheckXMLExist()
    {
        m_xmlPath = Application.dataPath + "/Atlas/Config/KikiBasicRhythm3.xml";

        //FileInfo file = new FileInfo(Application.persistentDataPath + "/" + name);

        if (!File.Exists(m_xmlPath))
        //if(!file.Exists)
        {
            m_rhythmXml = new XmlDocument();
            m_dec = m_rhythmXml.CreateXmlDeclaration("1.0", "utf-8", "yes");
            XmlNode configRoot = m_rhythmXml.CreateElement("Config");
            m_rhythmXml.AppendChild(configRoot);
            m_rhythmXml.Save(m_xmlPath);
            m_rhythmNodeList = m_rhythmXml.SelectSingleNode("Config").ChildNodes;            
        }
        else
        {
            m_rhythmXml = new XmlDocument();
            m_rhythmXml.Load(m_xmlPath);
            m_rhythmNodeList = m_rhythmXml.SelectSingleNode("Config").ChildNodes;
            
        }
    }

    public void InsertOneRhythm()
    {
        CheckXMLExist();
        XmlNode configRoot = m_rhythmXml.SelectSingleNode("Config");
        XmlElement element = m_rhythmXml.CreateElement("Rhythm");
        element.SetAttribute("Id", m_newRhythmID.ToString()); // (m_rhythmNodeList.Count+1).ToString());
        element.SetAttribute("Name", m_newRhythmName);

        int curBar = 0;
        foreach (AudioBar bar in EditorUIManager.GetInstance().AudioBarArray)
        {

            foreach (BaseBeat beat in bar.BeatList)
            {
                XmlElement elementChild = m_rhythmXml.CreateElement("Beat");
                elementChild.SetAttribute("Bar", curBar + "");
                elementChild.SetAttribute("Pos", beat.PlayPos+"");
                elementChild.SetAttribute("SoundType", beat.BeatIconType+"");
                elementChild.SetAttribute("Ya", "0");
                element.AppendChild(elementChild);
            }
            curBar++;
        }      
        configRoot.AppendChild(element);
        m_rhythmXml.AppendChild(configRoot);
        m_rhythmXml.Save(m_xmlPath);
    }


    private void LoadXml()
    {
        CheckXMLExist();       
        //遍历所有子节点
        foreach (XmlElement rhythmXml in m_rhythmNodeList)
        {
            Debug.Log("Rhythem ID:" + rhythmXml.GetAttribute("Id") + " Name:" + rhythmXml.GetAttribute("Name"));
            XmlNodeList xmlBeatList = rhythmXml.ChildNodes;
            foreach (XmlElement beatXml in xmlBeatList)
            {
                Debug.Log("-------------");
                Debug.Log(beatXml.Name +
                    "Bar:" + beatXml.GetAttribute("Bar") +
                    "Pos:" + beatXml.GetAttribute("Pos") +
                    "SoundType:" + beatXml.GetAttribute("SoundType") +
                    "Ya:" + beatXml.GetAttribute("Ya"));
            }
        }
    }
}
