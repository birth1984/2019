using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//重写Image类，让Image不参与射线检测。
public class ImagePlus : Image
{
    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        return false; 

        return base.IsRaycastLocationValid(screenPoint, eventCamera);
    }
}
