using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyHeroIcon : MonoBehaviour
{
    [SerializeField]
    private Image m_bg;

    [SerializeField]
    private Image m_icon;

    [SerializeField]
    private Image m_state;



    // Start is called before the first frame update
    void Start()
    {
        m_bg.sprite     = MyAtlasManager.Instance().GetIconSprite("hero_base_list_base_blank");
        m_icon.sprite   = MyAtlasManager.Instance().GetIconSprite("hero_icon_31");
        m_state.sprite  = MyAtlasManager.Instance().GetIconSprite("hero_card_lv_label_purple");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
