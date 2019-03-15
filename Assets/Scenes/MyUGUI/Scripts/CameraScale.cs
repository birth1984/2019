using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int ManualWidht = 1920;
        int ManualHeight = 1080;
        int manualHeight;

        if (System.Convert.ToSingle(Screen.height) / Screen.width > System.Convert.ToSingle(ManualHeight) / ManualWidht)
            manualHeight = Mathf.RoundToInt(System.Convert.ToSingle(ManualWidht) / Screen.width * Screen.height);
        else
            manualHeight = ManualHeight;

        Camera camera = GetComponent<Camera>();
        float scale = System.Convert.ToSingle(manualHeight / 1080f);
        camera.fieldOfView *= scale;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
