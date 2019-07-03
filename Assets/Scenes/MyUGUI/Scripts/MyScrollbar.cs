using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyScrollbar : MonoBehaviour
{
    [SerializeField]
    private Scrollbar m_scrollbarHorizontal;

    [SerializeField]
    private Scrollbar m_scrollbarVertical;

    [SerializeField]
    private Image m_image;
    // Start is called before the first frame update
    void Start()
    {
        if (m_scrollbarHorizontal)
        {
            m_scrollbarHorizontal.value = 0f;
            m_scrollbarHorizontal.onValueChanged.AddListener(onScroll);
        }
            
        if (m_scrollbarVertical)
        {
            m_scrollbarVertical.value = 1f;
            m_scrollbarVertical.onValueChanged.AddListener(onScroll);
        }                  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onScroll(float value)
    {
        Debug.Log("Scroll value is " + value.ToString());
        //m_image.fillAmount = value;
    }
}
