using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BaseBeat : MonoBehaviour
{
    [SerializeField]
    private Image m_normal;
    [SerializeField]
    private Image m_pressed;
    [SerializeField]
    private Text m_textUI;
    [SerializeField]
    private Image m_iconState;
    [SerializeField]
    private BeatIconType m_beatIconType;
    public BeatIconType BeatIconType { get => m_beatIconType; set => m_beatIconType = value; }

    [SerializeField]
    private AkAmbient[] m_akArr = new AkAmbient[3];
    [SerializeField]
    private float m_playPos = 0f;
    public float PlayPos { get => m_playPos; set => m_playPos = value; }
    [SerializeField]
    private bool m_hasPlayed = false;
    public bool HasPlayed { get => m_hasPlayed; set => m_hasPlayed = value; }

    /// <summary>
    /// Beat Data
    /// </summary>
    //[SerializeField]
    //private DjembBeat m_djembBeatData ;
    //public DjembBeat DjembBeatData { get => m_djembBeatData; set => m_djembBeatData = value; }
    //[SerializeField]
    //private List<BaseBeat> m_beatList;
    //public List<BaseBeat> BeatList { get => m_beatList; set => m_beatList = value; }
    public RectTransform RectTransform { get => m_rectTransform; set => m_rectTransform = value; }
    

    private RectTransform m_rectTransform;
    private EventTriggerListener m_eventTrigger;
    private Canvas m_editorUI;
    private Transform m_canvasTransform;
    private RectTransform m_canvasRectTrans;
    private AudioBar m_rootBar;
    public AudioBar RootBar { get => m_rootBar; set => m_rootBar = value; }
    private Vector2 m_dragPos;
    [SerializeField]
    private float m_note = 4 ; // 音符
    public float Note { get => m_note; set => m_note = value; }
    

    private ImageDoScale m_iScale;
    public ImageDoScale IScale { get => m_iScale; set => m_iScale = value; }

    private void Awake()
    {
        //m_beatList = new List<BaseBeat>();
        //m_beatList.Add(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_iScale = GetComponent<ImageDoScale>();
        m_textUI = GetComponentInChildren<Text>();
        Image[] imgArr = GetComponentsInChildren<Image>();
        m_normal = imgArr[0];
        m_pressed = imgArr[1];
        m_iconState = imgArr[2];      

        m_rectTransform = (RectTransform)transform;
        m_editorUI = EditorUIManager.GetInstance().EditorUI;
        m_canvasTransform = m_editorUI.transform;
        m_canvasRectTrans = (RectTransform)m_canvasTransform;

        m_pressed.transform.localPosition = new Vector3(m_normal.transform.localPosition.x + 1000 , m_normal.transform.localPosition.y , m_normal.transform.localPosition.z) ;
        //InitEventTrigger(false);

        m_akArr = GetComponents<AkAmbient>();
    }

    public void InitEventTrigger(bool b)
    {
        if(m_eventTrigger == null)
        {
            m_eventTrigger = EventTriggerListener.Get(this.gameObject);
        }
        if(b)
        {
            m_eventTrigger.onDown = OnBeatDown;
            m_eventTrigger.onDrag = OnBeatDrag;
            m_eventTrigger.onUp = OnBeatUp;
        }
        else
        {
            m_eventTrigger.onDown = OnPressed;
            m_eventTrigger.onDrag = null;
            m_eventTrigger.onUp = OnNormal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPressed(bool pressed)
    {
        if(pressed)
        {
            m_pressed.transform.localPosition = m_normal.transform.localPosition;
        }
        else
        {
            m_pressed.transform.localPosition 
                = new Vector3(m_normal.transform.localPosition.x + 1000, m_normal.transform.localPosition.y, m_normal.transform.localPosition.z);
        }
    }

    private void OnPressed(GameObject beat)
    {
        SetPressed(true);
    }
    private void OnNormal(GameObject beat)
    {
        SetPressed(false);
    }


    public void OnBeatDown(GameObject beat)
    {
        //切换层级关系
        transform.SetParent(m_canvasTransform);
        SetPressed(true);
        Debug.Log("On Beat Down  " + m_pressed.transform.localPosition + "   " + m_normal.transform.localPosition);        
    }
    public void OnBeatDrag(GameObject beat)
    {
        Debug.Log("On Beat Drag");
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(m_editorUI.transform as RectTransform, Input.mousePosition, m_editorUI.worldCamera, out m_dragPos))
        {
            m_dragPos.x -= m_rectTransform.rect.width * .5f;
            m_rectTransform.anchoredPosition = m_dragPos;
            //m_rectTransform.localPosition = m_dragPos;
            
        }
    }
    public void OnBeatUp(GameObject beat)
    {
        Debug.Log("On Beat Up");

        PlayBeat(0);

        SetPressed(false);

        if (EditorUIManager.RectOverLaps(m_rectTransform, m_rootBar.RectTransform))
        {
            //切换层级关系
            transform.SetParent(m_rootBar.transform);
            //EditorUIManager.GetInstance().ABar1.FreshBeat(EditorUIManager.GetInstance().ABar1.RectTransform , EditorUIManager.GetInstance().ABar1.BeatList);
            EditorUIManager.GetInstance().BarFresh();
        }
        else
        {
            // 没有交集 删除beat

            m_rootBar.RemoveBeat(this);
            DestoryBeat();
            m_rootBar.FreshBeat();
        }
    }

    public void PlayBeat(int id)
    {
        AkAmbient ak = null;
        if (m_akArr.Length >0)
            ak = m_akArr[id];
        if(ak != null )
            ak.data.Post(ak.gameObject);
        m_iScale.DoScale();
    }


    public void DestoryBeat()
    {
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    GameObject.Destroy(transform.GetChild(i).gameObject);
        //}
        //transform.DetachChildren();
        m_eventTrigger = null;
        Destroy(gameObject,0.1f);
    }

    //public void SetIconType(BeatIconType type)
    //{
    //    string prefabName = "";
    //    switch(type)
    //    {
    //        case BeatIconType.BEAT_DONG:
    //            prefabName = "common_tagPage1";
    //            break;
    //        case BeatIconType.BEAT_DU:
    //            prefabName = "common_tagPage";
    //            break;
    //        case BeatIconType.BEAT_DA:
    //            prefabName = "common_tagPage_gray";
    //            break;
    //        case BeatIconType.BEAT_NONE:
    //            prefabName = "common_tagPage_gray1";
    //            break;
    //    }

    //    GameObject spritePrefab = (GameObject)Resources.Load("Prefabs/Sprites/common/" + prefabName);//创建一个图片变量，路径为Resource/herominired的文件
    //    SpriteRenderer spr = spritePrefab.GetComponent<SpriteRenderer>();
    //    Debug.Log("m_iconState.sprite" + m_iconState + "   " + m_iconState.sprite);
    //    m_iconState.sprite = spr.sprite;
    //}
}
