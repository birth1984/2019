using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class MyAtlasManager
{
    private static MyAtlasManager _instance;

    public SpriteAtlas m_atlasHeroCard;

    public SpriteAtlas m_atlasHeroIcon;


    public static MyAtlasManager Instance()
    {
        if (_instance == null)
        {
            _instance = new MyAtlasManager();
            _instance.Init();
        }
            
        return _instance;
    }

    private void Init()
    {
        //以下必须放在Resource目录下 Resource目录下的所有资源将被打包到包体中
        m_atlasHeroCard = (SpriteAtlas)Resources.Load("SpriteAtlas/HeroCard");
        m_atlasHeroIcon = (SpriteAtlas)Resources.Load("SpriteAtlas/Icon");

        //如果需要动态加载或者打包到AB中(编辑器模式下使用)
        //m_atlasHeroIcon = AssetDatabase.LoadAssetAtPath<SpriteAtlas>("Assets/Resouses/SpriteAtlas/MyHeroIcon.spriteatlas");
    }

    public Sprite GetHeroCardSprite(string key)
    {
        return m_atlasHeroCard.GetSprite(key);
    }

    public Sprite GetIconSprite(string key)
    {
        return m_atlasHeroIcon.GetSprite(key);
    }
}
