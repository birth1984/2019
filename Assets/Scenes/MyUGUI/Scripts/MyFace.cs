using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MyFace : MonoBehaviour
{
    [SerializeField]
    public Image m_image;

    [SerializeField]
    public bool m_updateAlpha = false ;

    [MyFloatAttribute(1.0f, 0f)]
    public float m_updateAlphaValue = 0.5f;

    public float m_currAlphaValue = 0.5f ;

    private int m_alpahDir = 1;

    public void AlphaUpdate()
    {
        m_updateAlpha = !m_updateAlpha;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_currAlphaValue = m_image.canvasRenderer.GetAlpha();

        SpriteAtlas atlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>("Assets/SpriteAtlas/spriteAtlasHeroIcon.spriteatlas");
        Sprite sprite = atlas.GetSprite("hero_card_1");
        if (sprite != null)
        {
            //GetComponent<SpriteRenderer>().sprite = sprite;
            m_image.sprite = sprite;
        }
    }
        // Update is called once per frame
    void Update()
    {
        if(m_updateAlpha)
        {
            m_currAlphaValue = m_currAlphaValue + m_updateAlphaValue * Time.deltaTime * m_alpahDir;
            if(m_currAlphaValue >= 1 )
            {
                m_currAlphaValue = 1;
                //m_alpahDir = !m_alpahDir;
                m_alpahDir *= -1;
            }
            else if(m_currAlphaValue <=0)
            {
                m_currAlphaValue = 0;
                m_alpahDir *= -1;
            }
            m_image.canvasRenderer.SetAlpha(m_currAlphaValue) ; 
        }      
    }
}
