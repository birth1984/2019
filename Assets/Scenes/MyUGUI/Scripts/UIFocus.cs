using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 如果你想整体的关闭某个父节点下的所有UI事件。把如下脚本绑定在父节点上即可。
public class UIFocus : MonoBehaviour, ICanvasRaycastFilter
{
    public bool IsFocus = false;

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return IsFocus;
    }
}
