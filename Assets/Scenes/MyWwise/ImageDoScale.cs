using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImageDoScale : MonoBehaviour
{
    [SerializeField]
    private Image m_scaleImg;

    [SerializeField]
    private Vector3 m_from = new Vector3(1.2f, 1.2f, 1f);
    [SerializeField]
    private Vector3 m_to = new Vector3(1f, 1f, 1f);
    [SerializeField]
    private float m_duration = 0.1f;

    private Transform m_imgTransform;

    private Ease m_myEase = Ease.InOutBack;

    public Ease MyEase { get => m_myEase; set => m_myEase = value; }

    private float m_passTime;
    // Start is called before the first frame update
    void Start()
    {
        if(m_scaleImg == null)
            m_scaleImg = GetComponent<Image>();
        m_imgTransform = m_scaleImg.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //m_passTime += Time.deltaTime;
        //if(m_passTime>.5f)
        //{
        //    DoScale();
        //    m_passTime = 0;
        //}
    }

    public void DoScale()
    {
        //Debug.Log("移动完毕事件" + m_imgTransform);
        m_imgTransform.localScale = new Vector3(1.2f, 1.2f, 1f);
        Tweener tweener = m_imgTransform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
        //设置这个Tween不受Time.scale影响
        tweener.SetUpdate(true);
        //设置移动类型
        tweener.SetEase(m_myEase );
        tweener.onComplete = delegate ()
        {
            //Debug.Log("移动完毕事件");
        };

    }
}
