#if UNITY_ANDROID && !UNITY_EDITOR
#define ANDROID
#endif
 
 
#if UNITY_IPHONE && !UNITY_EDITOR
#define IPHONE
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.U2D;

//UGUI研究院之全面理解图集与使用（三）
public class UIMain : MonoBehaviour
{
    public GameObject m_mainCanvas;
    private Canvas m_canvas;
    private RectTransform m_rectTransform;
    public GameObject m_iconPanel;
    
    public Button button;
    public Image m_bgImage;
    public Image m_heroIcon;
    public Image m_heroIcon1;

    public SpriteAtlas m_Atlas;
    public SpriteRenderer m_spriteTest;
    // Start is called before the first frame update
    void Start()
    {
        m_spriteTest = GameObject.Find("RawImage").GetComponent<SpriteRenderer>();
        m_Atlas = (SpriteAtlas)Resources.Load("spriteAtlas/HeroCard");
        Sprite changeSprite = m_Atlas.GetSprite("hero_card_11");
        m_spriteTest.sprite = changeSprite;



        m_mainCanvas = GameObject.Find("MainCanvas");
        m_canvas = m_mainCanvas.GetComponent<Canvas>();
        m_iconPanel = GameObject.Find("IconPanel");

        button = GameObject.Find("Button").GetComponent<Button>();
        EventTriggerListener.Get(button.gameObject).onClick = OnButtonClick;
        button.onClick.AddListener(delegate () { this.OnButtonClick(button.gameObject); });
        button.onClick.AddListener(OnClick);

        m_bgImage = GameObject.Find("Bg").GetComponent<Image>();
        //EventTriggerListener.Get(m_bgImage.gameObject).onClick = OnButtonClick;
        //Tweener tweener = m_bgImage.rectTransform.DOMove(Vector3.zero, 2f);
        //tweener.SetUpdate<Tweener>(true);
        //tweener.SetEase(Ease.Linear);
        //tweener.onComplete = delegate ()
        //{
        //    Debug.Log("Tween Finish");
        //};
        //m_bgImage.material.DOFade(1f, 1f).onComplete = delegate ()
        //{
        //    Debug.Log("褪色Finish");
        //};

        DOTween.Init();
        m_heroIcon = CreateImage(m_mainCanvas.transform , LoadSprite("iconSprite", "hero_icon_3"));
        m_heroIcon1 = CreateImage(m_mainCanvas.transform , LoadSprite("iconSprite", "hero_icon_4"));

        
    }

    
    // Update is called once per frame
    void Update()
    {
        Vector2 pos;

        if ((m_rectTransform != null) &&  
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_canvas.transform as RectTransform, Input.mousePosition, m_canvas.worldCamera, out pos))
        {
            m_rectTransform.anchoredPosition = pos;
        }

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
//#if IPHONE || ANDROID
//            if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
//#else
//            if (EventSystem.current.IsPointerOverGameObject())
//#endif
//            {
//                Debug.Log("当前触摸在UI上");
//            }
//            else
//            {
//                Debug.Log("当前没有触摸在UI上");
//            }
        }
    }

    private Image CreateImage(Transform parent , Sprite sprite)
    {
        GameObject go = new GameObject(sprite.name);
        go.layer = LayerMask.NameToLayer("UI");
        go.transform.parent = parent;
        go.transform.localScale = Vector3.one;
        go.transform.position = new Vector3(0,0,10f);
        
        Image image = go.AddComponent<Image>();       
        image.sprite = sprite;
        image.SetNativeSize();

        EventTriggerListener.Get(go).onClick = OnClickImage;
        return image;
    }


    private Sprite LoadSprite(string fileName , string spriteName)
    {
        Sprite spt = Resources.Load<GameObject>("Prefabs/Sprites/" + fileName + "/" + spriteName).GetComponent<SpriteRenderer>().sprite;
        Debug.Log("LoadSprite: fileName: " + fileName + " spriteName " + spriteName + "   "+spt);
        return spt;
    }

    public void OnClick()
    {
        Debug.Log("0000000000000000");
        for (int i = 0; i < 10; i++)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/UI/HeroIconElement"));
            go.transform.parent = m_iconPanel.transform;
            go.transform.position = new Vector3(0, 0, 10f);
            go.transform.localScale = Vector3.one;
            go.GetComponent<Button>().onClick.AddListener(delegate () { this.OnIconClick(go); });
        }
    }


    public void OnIconClick(GameObject go)
    {
        go.GetComponent<LayoutElement>().ignoreLayout = true;
        go.SetActive(false);
    }
    public void OnButtonClick(GameObject go)
    {
        Debug.Log("DoSomeThings" + go.name);
        if (go == button.gameObject)
        {
            Debug.Log("DoSomeThings" + go.name);
        }
    }

    private string m_currSelectImageName = "";
    public void OnClickImage(GameObject image)
    {
        if(m_currSelectImageName.Equals(""))
        {
            m_currSelectImageName = image.name;
            m_rectTransform = image.transform as RectTransform;
        }
        else if(m_currSelectImageName.Equals(image.name))
        {
            m_currSelectImageName = "";
            m_rectTransform = null;
        }
        
    }
}
