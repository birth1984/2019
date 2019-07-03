using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWwiseType : MonoBehaviour
{
    public AK.Wwise.Bank MyUIBank = null;
    public AK.Wwise.Bank MyDjembBank = null;
    public AK.Wwise.Event MyEvent = null;
    public AK.Wwise.RTPC MyRTPC = null;

    public void Awake()
    {
        MyUIBank.Load();
        MyDjembBank.Load();
    }

    public void Start()
    {
        //MyEvent.Post(gameObject);
        MyEvent.Post(gameObject, MyCallbackFlags, EventCallback);
    }

    private float CalculateMyValue()
    {
        return (float)System.Math.Sin(System.Math.PI * UnityEngine.Time.timeSinceLevelLoad);
    }

    public void Update()
    {
        MyRTPC.SetValue(gameObject, CalculateMyValue());
    }

    public AK.Wwise.CallbackFlags MyCallbackFlags = null;


    private void EventCallback(object cookie, AkCallbackType type, AkCallbackInfo info)
    {
        if (type == AkCallbackType.AK_Marker)
        {
            var markerInfo = info as AkMarkerCallbackInfo;
            if (markerInfo != null)
            {
                // ...
            }
        }
    }
}
