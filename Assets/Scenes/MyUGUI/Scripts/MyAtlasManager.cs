using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class MyAtlasManager
{
    private static MyAtlasManager _instance;

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
        m_atlasHeroIcon = (SpriteAtlas)Resources.Load("SpriteAtlas/spriteAtlasHeroIcon");       
    }

    public Sprite GetHeroIconSprite(string key)
    {
        return m_atlasHeroIcon.GetSprite(key);
    }
}
