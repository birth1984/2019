using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayDjemb : MonoBehaviour
{
    [SerializeField]
    private Canvas m_beatRoot;
    [SerializeField]
    private Camera m_UICamera;

    [SerializeField]
    private Scrollbar m_scrollbarHorizontal;

    [SerializeField]
    private Text m_beatTxt;

    private float m_passSecond = 0f;
    // 节拍器速度
    [MyIntAttribute(240, 40)]    
    public int m_metronome = 80;
    private int m_lastMetronome;
    private float m_metronomeSecond;

    [SerializeField]
    private int m_beat = 4;
    private int m_currBeat = 0 ;

    [SerializeField]
    private AkAmbient m_AEvent;

    [SerializeField]
    private AkAmbient m_BEvent;

    [SerializeField]
    private AkAmbient m_dong1;
    [SerializeField]
    private AkAmbient m_dong2;

    [SerializeField]
    private AkAmbient m_du1;
    [SerializeField]
    private AkAmbient m_du2;

    [SerializeField]
    private AkAmbient m_da1;
    [SerializeField]
    private AkAmbient m_da2;

    [SerializeField]
    public List<DjembBeat> m_beatList ;
    [SerializeField]
    private DjembBeat[] m_beatArray = new DjembBeat[5];


    

    // Start is called before the first frame update
    void Start()
    {
        //m_btnAkEvent = GetComponent<AkAmbient>();
        m_lastMetronome = m_metronome;
        m_metronomeSecond = 60f / m_metronome;
        Debug.Log("m_scrollbarHorizontal " + m_scrollbarHorizontal.size);
        m_scrollbarHorizontal.size = 0.01f;
        if (m_scrollbarHorizontal)
        {
            m_scrollbarHorizontal.value = 0f;
            //m_scrollbarHorizontal.onValueChanged.AddListener(onScroll);
        }


        ////////////////////////////////////////
        foreach(DjembBeat db in m_beatArray)
        {
            //db = new DjembBeat();
            //m_beatOne.BeatType = 0;
            
            GameObject prefab   = (GameObject)Resources.Load(DjembBeat.GetBeatPath(db));
            db.Prefab           = Instantiate(prefab);           
            db.AkEvent          = db.Prefab.GetComponent<AkAmbient>();
            db.Img              = db.Prefab.GetComponent<Image>();
            db.ImgDoScale       = db.Prefab.GetComponent<ImageDoScale>();
            db.Prefab.transform.position = new Vector3(-105f + 60f * db.PlayPosition, DjembBeat.GetFloatHigh(db), 0f);
            //db.Prefab.transform.localScale = new Vector3(1f, 1f, 1f);
            db.Prefab.transform.SetParent(m_beatRoot.transform,false);
            
            
            
            
        }
        
    }

    // Update is called once per frame

    
    void Update()
    {      
        // 当节拍器速度变化时 重新计算
        if(m_metronome != m_lastMetronome)
        {
            m_lastMetronome = m_metronome;
            m_metronomeSecond = 60f / m_metronome;
        }

        

        // 过去的当时间满足节拍器时间播放节拍器音效
        if (m_passSecond >= m_metronomeSecond)
        {
            m_passSecond = 0;
            
            // 数拍子
            m_currBeat += 1;
            
            if (m_currBeat == m_beat)
            {
                m_currBeat = 0;
                m_scrollbarHorizontal.value = 0;
                foreach (DjembBeat db in m_beatArray)
                {
                    db.HasPlayed = false;                   
                }
            }           
        }

        foreach(DjembBeat db in m_beatArray)
        {
            if( !db.HasPlayed && m_currBeat * m_metronomeSecond + m_passSecond > m_metronomeSecond * db.PlayPosition )
            {
                db.AkEvent.data.Post(db.AkEvent.gameObject);
                db.HasPlayed = true;
                db.ImgDoScale.DoScale();
            }
        }
        

        m_beatTxt.text = "Beat:" + (m_currBeat+1);
        m_passSecond += Time.deltaTime;
        m_scrollbarHorizontal.value = m_passSecond / (m_metronomeSecond * m_beat) + float.Parse(m_currBeat.ToString()) / m_beat ; 
    }

    private void onScroll(float value)
    {
        Debug.Log("Scroll value is " + value.ToString());
        //m_image.fillAmount = value;
    }
}
