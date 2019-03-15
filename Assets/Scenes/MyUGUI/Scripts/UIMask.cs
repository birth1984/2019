using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMask : Mask  
{
    // Start is called before the first frame update
    void Start()
    {       
        base.Start();

        int width = Screen.width;
        int height = Screen.height;
        int designWidth = 1960;//开发时分辨率宽
        int designHeight = 1080;//开发时分辨率高
        float s1 = (float)designWidth / (float)designHeight;
        float s2 = (float)width / (float)height;

        //目标分辨率小于 960X640的 需要计算缩放比例
        float contentScale = 1f;
        if (s1 > s2)
        {
            contentScale = s1 / s2;
        }
        Canvas canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, transform.position, canvas.worldCamera, out pos))
        {
            ParticleSystemRenderer[] particlesSystems = transform.GetComponentsInChildren<ParticleSystemRenderer>();
            RectTransform rectTransform = transform as RectTransform;
            
            Vector3[] corners = new Vector3[4];           
            rectTransform.GetWorldCorners(corners);
            float minX, minY, maxX, maxY;
            minX = corners[0].x;
            minY = corners[0].y;
            maxX = corners[2].x;
            maxY = corners[2].y;

            //minX = rectTransform.rect.x + pos.x;
            //minY = rectTransform.rect.y + pos.y;
            //maxX = minX + rectTransform.rect.width;
            //maxY = minY + rectTransform.rect.height;

            //这里 100 是因为ugui默认的缩放比例是100 你也可以去改这个值，但是我觉得最好别改。
            foreach (ParticleSystemRenderer psr in particlesSystems)
            {
                psr.sharedMaterial.SetFloat("_MinX", minX / 100 / contentScale);
                psr.sharedMaterial.SetFloat("_MinY", minY / 100 / contentScale);
                psr.sharedMaterial.SetFloat("_MaxX", maxX / 100 / contentScale);
                psr.sharedMaterial.SetFloat("_MaxY", maxY / 100 / contentScale);
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
