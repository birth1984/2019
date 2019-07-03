using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioBar : MonoBehaviour
{
    [SerializeField]
    private Image m_overOnImg;
    private Vector3 m_defalutPos;
    private Vector3 m_overPos;
    private bool m_over = false ;
    private EventTriggerListener m_myListener;
    [SerializeField]
    private bool m_hasTriplet = false;
    public bool HasTriplet { get => m_hasTriplet; set => m_hasTriplet = value; }
    [SerializeField]
    private List<BaseBeat> m_beatList;
    private RectTransform m_rectTransform;
    [SerializeField]
    private BeatManager m_beatManager;

    public RectTransform RectTransform { get => m_rectTransform; set => m_rectTransform = value; }
    public List<BaseBeat> BeatList { get => m_beatList; set => m_beatList = value; }
    
    

    // Start is called before the first frame update
    void Start()
    {
        m_beatList = new List<BaseBeat>();

        m_rectTransform = (RectTransform)transform;

        m_overPos = m_overOnImg.transform.localPosition;
        m_defalutPos = new Vector3(m_overPos.x + 2000, m_overPos.y, m_overPos.z);
        m_overOnImg.transform.localPosition = m_defalutPos;

        //m_myListener = EventTriggerListener.Get(gameObject);
        //m_myListener.onEnter = OnBarEnter;
        //m_myListener.onExit = OnBarExit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBarEnter(GameObject obj = null )
    {
        if( !m_over )
        {
            m_over = true;
            m_overOnImg.transform.localPosition = m_overPos;
        }       
    }

    public void OnBarExit(GameObject obj = null)
    {
        if(m_over)
        {
            m_over = false;
            m_overOnImg.transform.localPosition = m_defalutPos;
        }       
    }

    public void AddBeat(BeatIconType iconType , BaseBeat colliderBeat = null)
    {        
        if (colliderBeat != null && colliderBeat.Note==32 )
            return;

        // 判断有没有连音
        float note = 0f;
        bool canInsertTriplet = true;
        if (m_beatList.Count > 3)
        {
            note = m_beatList[0].Note;
            foreach (BaseBeat beat in m_beatList)
            {
                if (note != beat.Note)
                {
                    note = beat.Note;
                    canInsertTriplet = false;
                }
            }
        }                   
        
        // 如果拖到拍子中 没有碰到其它beat 并且可以插入连音
        if(colliderBeat == null && !canInsertTriplet)
        {
            return;
        }
        

        BaseBeat newBeat = m_beatManager.CreateBeat(iconType);
        newBeat.transform.SetParent(transform, false);      
        newBeat.InitEventTrigger(true);
        newBeat.RootBar = this;

        if(colliderBeat!= null )
            m_beatList.Insert(m_beatList.IndexOf(colliderBeat) + 1, newBeat);       
        else
            m_beatList.Add(newBeat);
        
        if (colliderBeat == null)
        {
            foreach(BaseBeat beat in m_beatList)
            {
                beat.Note = 4 * m_beatList.Count;
                //if(m_beatList.Count % 2 == 0)
                //{
                //    m_hasTriplet = false;
                //}
                //else
                {
                    m_hasTriplet = true;
                }
            }            
        }            
        else
        {
            newBeat.Note = colliderBeat.Note * 2;
            colliderBeat.Note = newBeat.Note;
        }
        FreshBeat();
        //FreshBeat(m_rectTransform , m_beatList);
    }

    public void RemoveBeat(BaseBeat deletebeat)
    {
        if(m_hasTriplet)
        {
            m_beatList.Remove(deletebeat);
            foreach (BaseBeat beat in m_beatList)
            {
                beat.Note = 4 * m_beatList.Count;
                if (m_beatList.Count % 2 == 0)
                {
                    m_hasTriplet = false;
                }
                else
                {
                    m_hasTriplet = true;
                }
            }            
        }
        else
        {

            float log2 = Mathf.Log(deletebeat.Note , 2);
            float beatLog;
            foreach (BaseBeat beat in m_beatList)
            {
                beatLog = Mathf.Log(beat.Note, 2);
                if(beatLog >= log2)
                {
                    beat.Note = Mathf.Pow(2, beatLog - 1);
                }
            }
            m_beatList.Remove(deletebeat);
        }
        FreshBeat();      
    }

    public void FreshBeat()
    {
        float barWidth = m_rectTransform.rect.width;
        float beatWidth;
        float positionX = 0;
        foreach(BaseBeat beat in m_beatList)
        {
            beatWidth =  barWidth * (4f / beat.Note) ;
            beat.transform.localPosition = new Vector3(positionX, 0f, 0f);
            //Debug.Log("localPosition " + beat.transform.localPosition);
            RectTransform cRectTrans = (RectTransform)beat.transform;
            cRectTrans.sizeDelta = new Vector2(beatWidth, cRectTrans.rect.height);
            positionX += beatWidth;
        }

        float note = 0f;
        
        if (m_beatList.Count > 0)
        {
            m_hasTriplet = true;
            note = m_beatList[0].Note;
            float playPos = 0;
            foreach (BaseBeat beat in m_beatList)
            {
                if (note != beat.Note)
                {
                    note = beat.Note;
                    m_hasTriplet = false;                   
                }

                beat.PlayPos = playPos;
                playPos += 4f / beat.Note ;
            }            
        }        
    }

    //public void FreshBeatPos()
    //{
    //    float playPos = 0;
    //    foreach (BaseBeat beat in m_beatList)
    //    {
    //        beat.PlayPos = playPos;
    //        playPos += 4f / beat.Note;
    //    }
    //}
}
