using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorUIManager 
{
    private static EditorUIManager m_instance = null ;

    public static EditorUIManager GetInstance()
    {
        if(m_instance == null )
        {
            m_instance = new EditorUIManager();    
        }
        return m_instance;
    }

    private Canvas m_editorUI;
    private Scrollbar m_scrollbarHorizontal;
    private AudioBar m_audioBar1;
    private AudioBar m_audioBar2;
    private AudioBar m_audioBar3;
    private AudioBar m_audioBar4;
    private AudioBar[] m_audioBarArray;

    private Text m_bpmText;
    private Scrollbar m_bpmScrollbar;

    private float m_passSecond = 0f;
    // 节拍器速度
    [MyIntAttribute(150, 60)]
    private int m_BPM = 60;
    // private int m_lastMetronome;
    private float m_metronomeSecond;   
    private int m_countBar = 4;
    private int m_currBar = 0;

    public Canvas EditorUI { get => m_editorUI; set => m_editorUI = value; }
    public AudioBar AudioBar1 { get => m_audioBar1; set => m_audioBar1 = value; }
    public AudioBar AudioBar2 { get => m_audioBar2; set => m_audioBar2 = value; }
    public AudioBar AudioBar3 { get => m_audioBar3; set => m_audioBar3 = value; }
    public AudioBar AudioBar4 { get => m_audioBar4; set => m_audioBar4 = value; }
    public AudioBar[] AudioBarArray { get => m_audioBarArray; set => m_audioBarArray = value; }
    public float PassSecond { get => m_passSecond; set => m_passSecond = value; }
    public float MetronomeSecond { get => m_metronomeSecond; set => m_metronomeSecond = value; }
    public int CountBar { get => m_countBar; set => m_countBar = value; }
    public int CurrBar { get => m_currBar; set => m_currBar = value; }
    public Scrollbar ScrollbarHorizontal { get => m_scrollbarHorizontal; set => m_scrollbarHorizontal = value; }
    public int BPM { get => m_BPM; set => m_BPM = value; }

    EditorUIManager()
    {

    }


    public static bool RectOverLaps(RectTransform rt1, RectTransform rt2)
    {
        // 方法一
        //bool isIntersect = false;

        //Vector3 t1Pos = rt1.position;
        //Vector3 t1Scale = rt1.localScale;

        //Vector3 t2Pos = rt2.position;
        //Vector3 t2Scale = rt2.localScale;

        //float halfScale_X = t1Scale.x * .5f * rt1.rect.width + t2Scale.x * .5f * rt1.rect.width;
        //float halfScale_Y = t2Scale.y * .5f * rt2.rect.height + t2Scale.y * .5f * rt2.rect.height;

        //float distance_X = Mathf.Abs(t2Pos.x - t1Pos.x);
        //float distance_Y = Mathf.Abs(t2Pos.y - t1Pos.y);

        //if (distance_X <= halfScale_X && distance_Y <= halfScale_Y)
        //{
        //    isIntersect = true;
        //    //Debug.Log("相交");
        //}
        //else
        //{
        //    isIntersect = false;
        //    //Debug.Log("不想交");
        //}
        //return isIntersect;

        // 方法二
        //return rt1.rect.Overlaps(rt2.rect);

        // 方法三
        //bool isIntersect = false;
        //if( rt1.rect.Contains(new Vector2(rt2.rect.left ,   rt2.rect.top)) ||
        //    rt1.rect.Contains(new Vector2(rt2.rect.right ,  rt2.rect.top)) ||
        //    rt1.rect.Contains(new Vector2(rt2.rect.left ,   rt2.rect.bottom)) ||
        //    rt1.rect.Contains(new Vector2(rt2.rect.right ,  rt2.rect.bottom)))
        //{
        //    isIntersect = true;
        //}

        //return isIntersect;

        // 方法四
        Rect r1 = new Rect(rt1.position, rt1.rect.size);
        Rect r2 = new Rect(rt2.position, rt2.rect.size);
        if (r2.Overlaps(r1))
            return true;
        return false;       
    }


    public void BarFresh()
    {
        AudioBar1.FreshBeat();
        AudioBar2.FreshBeat();
        AudioBar3.FreshBeat();
        AudioBar4.FreshBeat();
    }

    public float ScrollbarValue()
    {
        return PassSecond / (MetronomeSecond * CountBar) + float.Parse(CurrBar.ToString()) / CountBar;
    }
        
    public float CountPassTime()
    {
        return /*CurrBar * MetronomeSecond +*/ PassSecond;
    }
}
