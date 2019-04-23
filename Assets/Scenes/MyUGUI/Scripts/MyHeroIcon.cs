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
        m_bg.sprite     = MyAtlasManager.Instance().GetHeroIconSprite("hero_base_list_blank");
        m_icon.sprite   = MyAtlasManager.Instance().GetHeroIconSprite("hero_card_1");
        m_state.sprite  = MyAtlasManager.Instance().GetHeroIconSprite("hero_card_lv_label_blue");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
