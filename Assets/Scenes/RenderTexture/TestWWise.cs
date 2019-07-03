using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWWise : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //AkGameObjectID MY_DEFAULT_LISTENER = 0;

        // 注册主要听者。
        //AK::SoundEngine::RegisterGameObj(MY_DEFAULT_LISTENER, "My Default Listener");

        // 将一个听者设置为默认。
        //AK::SoundEngine::SetDefaultListeners(&MY_DEFAULT_LISTENER, 1);

        // 注册游戏对象来播放声音
        //AkGameObjectID MY_EMITTER = 1;
        //AK::SoundEngine::RegisterGameObj(MY_EMITTER, "My Emitter");
        // 这时“My Emitter”有一个听者，即“My Default Listener”，因为我们已将其指派为默认听者。

        //AkGameObjectID MY_LISTENER_NO2 = 2;
        //AK::SoundEngine::RegisterGameObj(MY_LISTENER_NO2, "My Listener #2");

        // 如果我们只想为“My Emitter”变更听者，可以按以下方式：
        //AK::SoundEngine::SetListeners(MY_EMITTER, &MY_LISTENER_NO2, 1);

        // 这时，“My Emitter”有一个听者叫作“My Listener #2”。所有其它游戏对象仍然有“My Default Listener”作为它们的听者。
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
