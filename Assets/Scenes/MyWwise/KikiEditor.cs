using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml;
using System.IO;

public class KikiEditor : MonoBehaviour
{
    public Canvas m_editorUI;
    //public Button m_createDong;
    public Button m_createDongBtn;      
    public Button m_createDuBtn;
    public Button m_createDaBtn;
    public Button m_createNullBtn;
    public AudioBar m_audioBar1; 
    public AudioBar m_audioBar2;
    public AudioBar m_audioBar3;
    public AudioBar m_audioBar4;
    public AudioBar[] m_audioBarArr; //= new AudioBar[4];
    
    
    public BaseBeat m_dongBeat;
    private EventTriggerListener m_createDongBeatListener;
    public BaseBeat m_duBeat;
    private EventTriggerListener m_createDuBeatListener;
    public BaseBeat m_daBeat;
    private EventTriggerListener m_createDaBeatListener;
    public BaseBeat m_nopeBeat;
    private EventTriggerListener m_createNopeBeatListener;


    [SerializeField]
    private Scrollbar m_scrollbarHorizontal;
    [SerializeField]
    private Text m_bpmText;
    [SerializeField]
    private Scrollbar m_bpmScrollbar;

    public Image m_img1;
    public Image m_img2;
    private EventTriggerListener m_listenerImg1;
    private EventTriggerListener m_listenerImg2;


    private RectTransform m_rectTransform;
    //private string m_currSelectImageName = "";
    private Vector2 m_dragPos;

    private Object m_beatPrefab;
    //private GameObject m_tempBeat;
    private EventTriggerListener m_tempBeatListener;
    private AudioBar m_selectBar;
    public BeatManager m_beatManager;

   

    void Start()
    {
        m_beatManager = GetComponentInChildren<BeatManager>();

        m_editorUI = GetComponent<Canvas>();
        EditorUIManager.GetInstance().EditorUI = m_editorUI;
        EditorUIManager.GetInstance().ScrollbarHorizontal = m_scrollbarHorizontal;
        m_scrollbarHorizontal.size = 0.01f;
        if (m_scrollbarHorizontal)
        {
            m_scrollbarHorizontal.value = 0f;
            //m_scrollbarHorizontal.onValueChanged.AddListener(onScroll);
        }

        //m_beatBgArr[0] = m_aBar1;
        //m_beatBgArr[1] = m_aBar2;
        //m_beatBgArr[2] = m_aBar3;
        //m_beatBgArr[3] = m_aBar4;
        m_audioBarArr = new AudioBar[4] { m_audioBar1, m_audioBar2, m_audioBar3, m_audioBar4 };

        EditorUIManager.GetInstance().AudioBar1 = m_audioBar1;
        EditorUIManager.GetInstance().AudioBar2 = m_audioBar2;
        EditorUIManager.GetInstance().AudioBar3 = m_audioBar3;
        EditorUIManager.GetInstance().AudioBar4 = m_audioBar4;
        EditorUIManager.GetInstance().AudioBarArray = m_audioBarArr;

        m_dongBeat = m_beatManager.CreateBeat(BeatIconType.BEAT_DONG);
        m_dongBeat.transform.SetParent(this.transform, false);
        m_dongBeat.transform.localPosition = m_createDongBtn.transform.localPosition;
       
        m_createDongBeatListener = EventTriggerListener.Get(m_dongBeat.gameObject);
        m_createDongBeatListener.onDown = OnBeatDown;
        m_createDongBeatListener.onDrag = OnBeatDrag;
        m_createDongBeatListener.onUp = OnBeatUp;


        m_duBeat = m_beatManager.CreateBeat(BeatIconType.BEAT_DU);
        m_duBeat.transform.SetParent(this.transform, false);
        m_duBeat.transform.localPosition = m_createDuBtn.transform.localPosition;

        m_createDuBeatListener = EventTriggerListener.Get(m_duBeat.gameObject);
        m_createDuBeatListener.onDown = OnBeatDown;
        m_createDuBeatListener.onDrag = OnBeatDrag;
        m_createDuBeatListener.onUp = OnBeatUp;


        m_daBeat = m_beatManager.CreateBeat(BeatIconType.BEAT_DA);
        m_daBeat.transform.SetParent(this.transform, false);
        m_daBeat.transform.localPosition = m_createDaBtn.transform.localPosition;

        m_createDaBeatListener = EventTriggerListener.Get(m_daBeat.gameObject);
        m_createDaBeatListener.onDown = OnBeatDown;
        m_createDaBeatListener.onDrag = OnBeatDrag;
        m_createDaBeatListener.onUp = OnBeatUp;


        m_nopeBeat = m_beatManager.CreateBeat(BeatIconType.BEAT_NONE);
        m_nopeBeat.transform.SetParent(this.transform, false);
        m_nopeBeat.transform.localPosition = m_createNullBtn.transform.localPosition;

        m_createNopeBeatListener = EventTriggerListener.Get(m_nopeBeat.gameObject);
        m_createNopeBeatListener.onDown = OnBeatDown;
        m_createNopeBeatListener.onDrag = OnBeatDrag;
        m_createNopeBeatListener.onUp = OnBeatUp;


        m_listenerImg1 = EventTriggerListener.Get(m_img1.gameObject);
        //m_listenerImg1.onClick = OnClickImage;
        m_listenerImg1.onBeginDrag = OnBeginDrag;
        m_listenerImg1.onDrag = OnDrag;
        m_listenerImg1.onEndDrag = OnEndDrag;

        //m_img2 = imageArr[1];
        m_listenerImg2 = EventTriggerListener.Get(m_img2.gameObject);
        m_listenerImg2.onClick = SaveAudio;


        // BPM
        m_bpmText.text = "BPP:" + EditorUIManager.GetInstance().BPM;
        m_bpmScrollbar.size = 0.05f;
        m_bpmScrollbar.numberOfSteps = 19;
        m_bpmScrollbar.onValueChanged.AddListener(onBpmChange);

        Debug.Log("---" + Mathf.Log(4,2));
        Debug.Log("---" + Mathf.Log(8,2));
        Debug.Log("---" + Mathf.Log(16,2));
        Debug.Log("---" + Mathf.Log(32,2));

        Debug.Log("---" + Mathf.Pow(2,2));
        Debug.Log("---" + Mathf.Pow(2,3));
        Debug.Log("---" + Mathf.Pow(2,4));
        Debug.Log("---" + Mathf.Pow(2,5));
    }

    // Update is called once per frame
    void Update()
    {
        m_scrollbarHorizontal.value = EditorUIManager.GetInstance().ScrollbarValue();
        return;
        if ((m_rectTransform != null) &&
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_editorUI.transform as RectTransform, Input.mousePosition, m_editorUI.worldCamera, out m_dragPos))
        {
            m_rectTransform.anchoredPosition = m_dragPos;

            //Debug.Log("当前触摸Pos:" + m_dragPos + "NewRect " + m_rectTransform + "  BarRect: " + ((RectTransform)m_aBar1.transform).rect);
            if ((m_rectTransform != null) &&
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_editorUI.transform as RectTransform, Input.mousePosition, m_editorUI.worldCamera, out m_dragPos))
            {
                m_rectTransform.anchoredPosition = m_dragPos;                
                if (EditorUIManager.RectOverLaps(m_rectTransform, (RectTransform)m_audioBar1.transform))
                {
                    m_audioBar1.OnBarEnter();
                    //m_selectBar = bar;
                    
                    
                }
                else
                {
                    m_audioBar1.OnBarExit();
                }
            }
        }

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
#if IPHONE || ANDROID
            if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
            if (EventSystem.current.IsPointerOverGameObject())
#endif
            {
                              
            }
            else
            {
                Debug.Log("当前没有触摸在UI上");
            }
            
        }
    }



    public void OnBeatDown(GameObject beat)
    {
        //m_currSelectImageName = beat.name;
        m_rectTransform = beat.transform as RectTransform;
        BaseBeat scriptBeat = beat.GetComponent<BaseBeat>();
        scriptBeat.SetPressed(true);
        scriptBeat.RectTransform.sizeDelta = new Vector2(30f, 30f);
        scriptBeat.PlayBeat(0);
        Debug.Log("On Down Down Down" + m_rectTransform.rect);  
    }

    private BaseBeat m_childBeat ;
    private float m_childBeatPosX;
    public void OnBeatDrag(GameObject beat)
    {
        Debug.Log("On DRAG DRAG DRAG");
                
        if ((m_rectTransform != null) &&
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_editorUI.transform as RectTransform, Input.mousePosition, m_editorUI.worldCamera, out m_dragPos))
        {
            m_dragPos.x -= m_rectTransform.rect.width * .5f;
            m_rectTransform.anchoredPosition = m_dragPos;
            foreach (AudioBar audioBar in m_audioBarArr)
            {
                if (EditorUIManager.RectOverLaps(m_rectTransform, (RectTransform)audioBar.transform))
                {
                    audioBar.OnBarEnter();
                }
                else
                {
                    audioBar.OnBarExit();
                }

                float tempWidth;
                int tempCount = 0;
                float tempPosx;

                if (audioBar.BeatList.Count == 0)
                {
                    if (EditorUIManager.RectOverLaps(m_rectTransform, (RectTransform)audioBar.transform))
                    {
                        tempWidth = audioBar.RectTransform.rect.width / (audioBar.BeatList.Count + 1);
                        tempCount = 0;
                        foreach (BaseBeat cBeat in audioBar.BeatList)
                        {
                            cBeat.transform.localPosition = new Vector3(tempWidth * tempCount, 0f, 0f);
                            RectTransform cRectTrans = (RectTransform)cBeat.transform;
                            cRectTrans.sizeDelta = new Vector2(tempWidth, cRectTrans.rect.height);
                            tempCount++;
                        }
                        tempCount = 0;
                    }
                    else
                    {
                        audioBar.FreshBeat();
                    }
                }
                else
                {
                    foreach (BaseBeat childBeat in audioBar.BeatList)
                    {
                        if (EditorUIManager.RectOverLaps(m_rectTransform, (RectTransform)childBeat.transform))
                        {
                            if (m_childBeat && !m_childBeat.Equals(childBeat))
                            {
                                m_childBeat.SetPressed(false);
                            }
                            m_childBeat = childBeat;
                            childBeat.SetPressed(true);
                        }
                        else
                        {
                            childBeat.SetPressed(false);
                        }
                    }

                    if (m_childBeat)
                    {
                        audioBar.FreshBeat();
                    }
                    else
                    {
                        if (EditorUIManager.RectOverLaps(m_rectTransform, (RectTransform)audioBar.transform))
                        {
                            tempWidth = audioBar.RectTransform.rect.width / (audioBar.BeatList.Count + 1);
                            tempCount = 0;
                            foreach (BaseBeat cBeat in audioBar.BeatList)
                            {
                                cBeat.transform.localPosition = new Vector3(tempWidth * tempCount, 0f, 0f);
                                RectTransform cRectTrans = (RectTransform)cBeat.transform;
                                cRectTrans.sizeDelta = new Vector2(tempWidth, cRectTrans.rect.height);
                                tempCount++;
                            }
                            tempCount = 0;
                        }
                        else
                        {
                            audioBar.FreshBeat();
                        }
                    }
                }
            }   
        }
        m_childBeat = null;
    }


    public void OnBeatUp(GameObject beat)
    {
        Debug.Log("On UP UP UP");
        BaseBeat scriptBeat = beat.GetComponent<BaseBeat>();

        foreach (AudioBar audioBar in m_audioBarArr)
        {
            if (EditorUIManager.RectOverLaps(m_rectTransform, (RectTransform)audioBar.transform))
            {
                foreach (BaseBeat childBeat in audioBar.BeatList)
                {
                    if (EditorUIManager.RectOverLaps(m_rectTransform, (RectTransform)childBeat.transform))
                    {
                        if (m_childBeat && !m_childBeat.Equals(childBeat))
                        {
                            m_childBeat.SetPressed(false);
                        }
                        m_childBeat = childBeat;
                        childBeat.SetPressed(true);
                    }
                    else
                    {
                        childBeat.SetPressed(false);
                    }
                }

                if (m_childBeat != null)
                {
                    audioBar.AddBeat(scriptBeat.BeatIconType, m_childBeat);
                }
                else
                {
                    audioBar.AddBeat(scriptBeat.BeatIconType);
                }
            }
            audioBar.OnBarExit();
        }
                            
        
        
        scriptBeat.SetPressed(false);
        RectTransform rectTrans;
        switch (scriptBeat.BeatIconType)
        {
            case BeatIconType.BEAT_DONG:
                rectTrans = (RectTransform)m_createDongBtn.transform;
                m_rectTransform.anchoredPosition = rectTrans.anchoredPosition;
                beat.transform.localPosition = m_createDongBtn.transform.localPosition;
                scriptBeat.RectTransform.sizeDelta = rectTrans.rect.size;
                break;
            case BeatIconType.BEAT_DU:
                rectTrans = (RectTransform)m_createDuBtn.transform;
                m_rectTransform.anchoredPosition = rectTrans.anchoredPosition;
                beat.transform.localPosition = m_createDuBtn.transform.localPosition;
                scriptBeat.RectTransform.sizeDelta = rectTrans.rect.size;
                break;
            case BeatIconType.BEAT_DA:
                rectTrans = (RectTransform)m_createDaBtn.transform;
                m_rectTransform.anchoredPosition = rectTrans.anchoredPosition;
                beat.transform.localPosition = m_createDaBtn.transform.localPosition;
                scriptBeat.RectTransform.sizeDelta = rectTrans.rect.size;
                break;
            case BeatIconType.BEAT_NONE:
                rectTrans = (RectTransform)m_createNullBtn.transform;
                m_rectTransform.anchoredPosition = rectTrans.anchoredPosition;
                beat.transform.localPosition = m_createNullBtn.transform.localPosition;
                scriptBeat.RectTransform.sizeDelta = rectTrans.rect.size;
                break;

        }
        
        
        
        

        //m_currSelectImageName = "";
        m_rectTransform = null;

        if(m_childBeat != null)
        {
            m_childBeat.SetPressed(false);
            m_childBeat = null;
        }

        ///Debug.Log("Fffffffffffffresh.......");
        EditorUIManager.GetInstance().BarFresh();
        //m_aBar1.FreshBeat(m_aBar1.RectTransform, m_aBar1.BeatList);
        return;        
    }


    
    public void SaveAudio(GameObject image)
    {
        m_img2.GetComponent<ImageDoScale>().DoScale();
        //CheckXMLExist();
        //InsertOneRhythm();
    }

    

    public void OnBeginDrag(GameObject image)
    {
        Debug.Log("Drag : " + image.name);       
        //m_currSelectImageName = image.name;
        m_rectTransform = image.transform as RectTransform;
    }

    public void OnDrag(GameObject image)
    {
        //Debug.Log("当前触摸在UI上" + m_dragPos + "  " + m_img2.rectTransform);
        if ((m_rectTransform != null) &&
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_editorUI.transform as RectTransform, Input.mousePosition, m_editorUI.worldCamera, out m_dragPos))
        {
            m_rectTransform.anchoredPosition = m_dragPos;
            
            if (EditorUIManager.RectOverLaps(m_rectTransform, m_audioBar1.GetComponent<Image>().rectTransform))
            {
                m_audioBar1.OnBarEnter();
                //m_selectBar = bar;
            }
            else
            {
                m_audioBar1.OnBarExit();
            }
        }

        //if (m_currSelectImageName.Equals("Image"))
        //{
       
        //}       
    }

    public void OnEndDrag(GameObject image)
    {
        EditorUIManager.RectOverLaps(m_img1.rectTransform, m_img2.rectTransform);
        //m_currSelectImageName = "";
        m_rectTransform = null;
    }

    private void onBpmChange(float value)
    {        
        EditorUIManager.GetInstance().BPM = int.Parse( (60 + value * 90) + "") ;
        m_bpmText.text = "BPP:" + EditorUIManager.GetInstance().BPM;
        Debug.Log("BPM:" + (60 + value * 90));
    }
    
}
