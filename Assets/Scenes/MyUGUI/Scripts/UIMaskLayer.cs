using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UGUI研究院之设置全屏图（十）
//把如下脚本挂在需要全屏的Image对象上即可。
public class UIMaskLayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int m_designWidth = 1960;
        int m_designHeight = 1080;
        float s1 = m_designWidth / m_designHeight;
        float s2 = Screen.width / Screen.height;

        if(s1 < s2)
        {
            m_designWidth = (int)Mathf.FloorToInt(m_designHeight * s2);
        }
        else if(s1>s2)
        {
            m_designHeight = (int)Mathf.FloorToInt(m_designWidth / s2);
        }
        float contentScale = (float)m_designWidth / (float)Screen.width;
        RectTransform rectTransform = transform as RectTransform;
        if(rectTransform != null)
        {
            rectTransform.sizeDelta = new Vector2(m_designWidth, m_designHeight);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
