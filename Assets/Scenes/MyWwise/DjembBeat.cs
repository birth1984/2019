using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DjembBeat// : MonoBehaviour
{
    [SerializeField]
    private BeatType m_beatType = BeatType.BEAT_LOW_LEFT;
    [SerializeField]
    private BeatIconType m_beatIconType = BeatIconType.BEAT_NONE;
    [SerializeField]
    private GameObject m_prefab;
    [SerializeField]
    private AkAmbient m_akEvent;
    [SerializeField]
    private Image m_img;
    [SerializeField]
    private ImageDoScale m_imgDoScale;
    [SerializeField]
    private float m_playPosition; // 播放位置    
    [SerializeField]
    private bool m_hasPlayed = false;


    public BeatType BeatType { get => m_beatType; set => m_beatType = value; }
    public GameObject Prefab { get => m_prefab; set => m_prefab = value; }
    public AkAmbient AkEvent { get => m_akEvent; set => m_akEvent = value; }
    public Image Img { get => m_img; set => m_img = value; }
    public bool HasPlayed { get => m_hasPlayed; set => m_hasPlayed = value; }
    public ImageDoScale ImgDoScale { get => m_imgDoScale; set => m_imgDoScale = value; }
    public float PlayPosition { get => m_playPosition; set => m_playPosition = value; }
    public BeatIconType BeatIconType { get => m_beatIconType; set => m_beatIconType = value; }

    ////DjembBeat() { }
    //void Start()
    //{

    //}

    public static string GetBeatTypeName(DjembBeat db)
    {
        string beatTypeName = "";
        switch (db.BeatType)
        {
            case BeatType.BEAT_LOW_LEFT:    beatTypeName = "BeatLowLeft";   break;
            case BeatType.BEAT_LOW_RIGHT:   beatTypeName = "BeatLowRight";  break;
            case BeatType.BEAT_LOW_Z:       beatTypeName = "BeatLowZ";      break;
            case BeatType.BEAT_MID_LEFT:    beatTypeName = "BeatMidLeft";   break;
            case BeatType.BEAT_MID_RIGHT:   beatTypeName = "BeatMidRight";  break;
            case BeatType.BEAT_MID_Z:       beatTypeName = "BeatMidZ";      break;
            case BeatType.BEAT_HIG_LEFT:    beatTypeName = "BeatHigLeft";   break;
            case BeatType.BEAT_HIG_RIGHT:   beatTypeName = "BeatHigRight";  break;
            case BeatType.BEAT_HIG_Z:       beatTypeName = "BeatHigZ";      break;

        }
        return beatTypeName;
    }

    public static string GetBeatPath(DjembBeat db)
    {
        return "Prefabs/Djemb/Beat/" + GetBeatTypeName(db);
    }

    public static float GetFloatHigh(DjembBeat db)
    {
        float high = 0f;
        switch (db.BeatType)
        {
            case BeatType.BEAT_LOW_LEFT:    high = 60f; break;
            case BeatType.BEAT_LOW_RIGHT:   high = 60f; break;
            case BeatType.BEAT_LOW_Z:       high = 60f; break;
            case BeatType.BEAT_MID_LEFT:    high = 30f; break;
            case BeatType.BEAT_MID_RIGHT:   high = 30f; break;
            case BeatType.BEAT_MID_Z:       high = 30f; break;
            case BeatType.BEAT_HIG_LEFT:    high = 0f; break;
            case BeatType.BEAT_HIG_RIGHT:   high = 0f; break;
            case BeatType.BEAT_HIG_Z:       high = 0f; break;

        }
        return high;
    }

}
