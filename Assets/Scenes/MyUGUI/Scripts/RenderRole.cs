using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderRole : MonoBehaviour
{
    // 设置SpriteRender与Camera之间的距离
    [SerializeField]
    private float m_distance = 1.0f;

    [SerializeField]
    private SpriteRenderer m_roleSpt = null;

    [SerializeField]
    private Camera m_roleCamera;

    [SerializeField]
    private RenderTexture m_renderTexture;

    [SerializeField]
    private Image m_image;

    // Start is called before the first frame update
    void Start()
    {
        m_roleSpt = GetComponent<SpriteRenderer>();
        m_roleSpt.material.renderQueue = 2980;
        RenderTexture.active = m_renderTexture;

        Texture2D screenShot = new Texture2D(256, 256, TextureFormat.RGB24, false);
        //读取
        screenShot.ReadPixels(new Rect(0,0,256,256) , 0, 0);
        screenShot.Apply();
        //创建精灵
        Sprite spr = Sprite.Create(screenShot, new Rect(0, 0, 256, 256), Vector2.zero);
        m_roleSpt.sprite = spr;
    }

    // Update is called once per frame
    void Update()
    {
        //RenderTexture.active = m_renderTexture;
        //Texture2D screenShot = new Texture2D(256, 256, TextureFormat.RGB24, false);
        ////读取
        //screenShot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        //screenShot.Apply();
        ////创建精灵
        //Sprite spr = Sprite.Create(screenShot, new Rect(0, 0, 256, 256), Vector2.zero);
        //m_image.sprite = spr;
        //m_roleCamera.transform.rotation = Quaternion.Euler(Vector3.zero);
        //float width = m_roleSpt.sprite.bounds.size.x;
        //float height = m_roleSpt.sprite.bounds.size.y;

        //float worldScreenHeight, worldScreenWidth;

        //if (m_roleCamera.orthographic)
        //{
        //    worldScreenHeight = m_roleCamera.orthographicSize * 2.0f;
        //    worldScreenWidth = Screen.width * worldScreenHeight / Screen.height;
        //}
        //else
        //{
        //    worldScreenHeight = 2.0f * m_distance * Mathf.Tan(m_roleCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        //    worldScreenWidth = worldScreenHeight * m_roleCamera.aspect;
        //}
        //transform.localPosition = new Vector3(m_roleCamera.transform.position.x, m_roleCamera.transform.position.y, m_distance);
        //transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, 0f);
    }
}
