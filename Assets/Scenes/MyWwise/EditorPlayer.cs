using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPlayer : MonoBehaviour
{    
    private int m_lastMetronome;
    private AudioBar m_currBar;

    // Start is called before the first frame update
    void Start()
    {
        m_currBar = EditorUIManager.GetInstance().AudioBarArray[EditorUIManager.GetInstance().CurrBar];
    }

    

    // Update is called once per frame
    void Update()
    {
        // 当节拍器速度变化时 重新计算
        if (EditorUIManager.GetInstance().BPM != m_lastMetronome)
        {
            m_lastMetronome = EditorUIManager.GetInstance().BPM;
            EditorUIManager.GetInstance().MetronomeSecond = 60f / EditorUIManager.GetInstance().BPM;
        }
        
        // 过去的当时间满足节拍器时间播放节拍器音效
        if (EditorUIManager.GetInstance().PassSecond >= EditorUIManager.GetInstance().MetronomeSecond)
        {
            EditorUIManager.GetInstance().PassSecond = 0;

            // 数拍子
            EditorUIManager.GetInstance().CurrBar += 1;

            if (EditorUIManager.GetInstance().CurrBar == EditorUIManager.GetInstance().CountBar)
            {
                EditorUIManager.GetInstance().CurrBar = 0;
                EditorUIManager.GetInstance().ScrollbarHorizontal.value = 0;
                foreach (AudioBar audioBar in EditorUIManager.GetInstance().AudioBarArray)
                {
                    foreach(BaseBeat beat in audioBar.BeatList)
                    {
                        beat.HasPlayed = false;
                    }                   
                }
            }
            m_currBar = EditorUIManager.GetInstance().AudioBarArray[EditorUIManager.GetInstance().CurrBar];
        }

        
        
        foreach(BaseBeat beat in m_currBar.BeatList)
        {
            if (!beat.HasPlayed &&
            EditorUIManager.GetInstance().CountPassTime() > EditorUIManager.GetInstance().MetronomeSecond * beat.PlayPos)
            {
                beat.PlayBeat(0);
                beat.HasPlayed = true;
                //beat.ImgDoScale.DoScale();
            }
        }

        //m_beatTxt.text = "Beat:" + (m_currBeat + 1);
        EditorUIManager.GetInstance().PassSecond += Time.deltaTime;
        //m_scrollbarHorizontal.value = m_passSecond / (m_metronomeSecond * m_beat) + float.Parse(m_currBeat.ToString()) / m_beat;
    }
}
