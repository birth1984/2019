using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyScrollbar : MonoBehaviour
{
    [SerializeField]
    private Scrollbar m_scrollbar;
    [SerializeField]
    private Image m_image;
    // Start is called before the first frame update
    void Start()
    {
        m_scrollbar.onValueChanged.AddListener(onScroll);
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
