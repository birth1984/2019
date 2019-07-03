using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CtrlChildScrollView : MonoBehaviour , IBeginDragHandler , IDragHandler , IEndDragHandler
{
    [SerializeField]
    private ScrollRect m_rootSR;   

    [SerializeField]
    private ScrollRect m_childSR;

    [SerializeField]
    private bool m_heroCardDragging = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        return;
        //m_rootSR.OnBeginDrag(eventData);
        m_childSR.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        return;
        //m_rootSR.OnDrag(eventData);
        m_childSR.OnDrag(eventData);

        float angle = Vector2.Angle(eventData.delta, Vector2.up);

        if (angle > 45f && angle < 135f)
        {
            Debug.Log("left and right");
            m_heroCardDragging = true;
            m_rootSR.enabled = !m_heroCardDragging;                       
        }
        else
        {
            Debug.Log("up and down");
            m_heroCardDragging = false;
            m_rootSR.enabled = !m_heroCardDragging;           
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        return;
        //m_rootSR.OnEndDrag(eventData);
        m_childSR.OnEndDrag(eventData);        
        m_rootSR.enabled = true;

        if (m_heroCardDragging)
        {
            m_heroCardDragging = false;
            m_rootSR.enabled = true;
        }
    }
}
