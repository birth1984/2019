using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BtnAddListener : MonoBehaviour 
{
    [SerializeField]
    private Button m_button;

    [SerializeField]
    private AkAmbient m_btnAkEvent;
    private Image m_image;

    // Start is called before the first frame update
    void Start()
    {
        m_button.onClick.AddListener(OnClickButton);
        m_btnAkEvent = GetComponent<AkAmbient>();
        m_image = GetComponent<Image>();
        //m_btnAkEvent.actionOnEventType = AkActionOnEventType.AkActionOnEventType_Pause;   
    }

    public void OnClickButton()
    {
        //Debug.Log("Button is clicked" + m_beatImg.rectTransform.rect);     
       // m_btnAkEvent.data.Post(m_btnAkEvent.gameObject);       

        m_image.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        Tweener tweImg = m_image.rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
        //设置这个Tween不受Time.scale影响
        tweImg.SetUpdate(true);
        //设置移动类型
        tweImg.SetEase(Ease.InOutBack);
        //tweImg.onComplete = delegate ()
        //{
        //    Debug.Log("移动完毕事件");
        //};

        //m_beatImg.GetComponent<Animator>().SetBool("Normal" , true);//  .SetFloat("ImgScale", 0.1f);
    }

    
}
