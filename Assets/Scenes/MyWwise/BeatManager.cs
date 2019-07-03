using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    private static BeatManager m_instance = null;
    private Object m_prefabDone;
    private Object m_prefabDu;
    private Object m_prefabDa;
    private Object m_prefabNone;

    private void Awake()
    {
        m_prefabDone = Resources.Load("Prefabs/Djemb/Beat/BeatDone");
        m_prefabDu = Resources.Load("Prefabs/Djemb/Beat/BeatDu");
        m_prefabDa = Resources.Load("Prefabs/Djemb/Beat/BeatDa");
        m_prefabNone = Resources.Load("Prefabs/Djemb/Beat/BeatNone");
    }

    private void Start()
    {    
        //m_prefabDone    = Resources.Load("Prefabs/Djemb/BeatDone");
        //m_prefabDu      = Resources.Load("Prefabs/Djemb/BeatDu");
        //m_prefabDa      = Resources.Load("Prefabs/Djemb/BeatDa");
        //m_prefabNone    = Resources.Load("Prefabs/Djemb/BeatNone");
    }

    public BaseBeat CreateBeat(BeatIconType bIType)
    {
        GameObject gameObj = null;
        switch (bIType)
        {
            case BeatIconType.BEAT_DONG:
                gameObj = (GameObject)Instantiate(m_prefabDone);
                break;
            case BeatIconType.BEAT_DU:
                gameObj = (GameObject)Instantiate(m_prefabDu);
                break;
            case BeatIconType.BEAT_DA:
                gameObj = (GameObject)Instantiate(m_prefabDa);
                break;
            case BeatIconType.BEAT_NONE:
                gameObj = (GameObject)Instantiate(m_prefabNone);
                break;
        }
        return gameObj.GetComponent<BaseBeat>();
    }
}

public class BeatManagerObject : MonoBehaviour
{
    public BeatManager m_bManager;
}