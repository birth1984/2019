using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.Animations;


public class SpritAnimationCreator : MonoBehaviour
{
    private static float defaultInterval = 0.1f;

    [MenuItem("BirthEditor/Create/Sprite Animation")]
    public static void CreateSpriteAnimation()
    {
        List<Sprite> selectedSprites = new List<Sprite>(
                Selection.GetFiltered(typeof(Sprite) , SelectionMode.TopLevel).OfType<Sprite>()
            );


        //如果选中纹理，则获取其中的精灵
        //if (selectedSprites.Count == 0)
        //{
            //object[] selectedTextures =
            //Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);

            //foreach (Object texture in selectedTextures)
            //{
            //    selectedSprites.AddRange(AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(texture)).OfType<Sprite>());
            //}
        //}


        if (selectedSprites.Count < 1 )
        {
            Debug.LogWarning("No sprite selected.");
            return;
        }

        string suffixPattern = "_?([0-9]+)$";
        selectedSprites.Sort(
                (Sprite _1, Sprite _2) =>
                {
                    Match match1 = Regex.Match(_1.name, suffixPattern);
                    Match match2 = Regex.Match(_2.name, suffixPattern);
                    if (match1.Success && match2.Success)
                    {
                        return (int.Parse(match1.Groups[1].Value) - int.Parse(match2.Groups[1].Value)) ;
                    }
                    else
                    {
                        return _1.name.CompareTo(_2.name);
                    }
                }
            );

        string baseDir = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedSprites[0]));

        string baseName = Regex.Replace(selectedSprites[0].name, suffixPattern, "");

        if(string.IsNullOrEmpty(baseName))
        {
            baseName = selectedSprites[0].name;
        }

        Canvas canvas = FindObjectOfType<Canvas>();
        if(canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            canvasObj.layer = LayerMask.NameToLayer("UI");
        }

        //创建图像
        GameObject obj = new GameObject(baseName);
        obj.transform.parent = canvas.transform;
        obj.transform.localPosition = Vector3.zero;

        Image image = obj.AddComponent<Image>();
        image.sprite = (Sprite)selectedSprites[0];
        image.SetNativeSize();

        //附加Animator组件
        Animator animator = obj.AddComponent<Animator>();

        //创建动画剪辑
        AnimationClip animationClip = AnimatorController.AllocateAnimatorClip(baseName);

        //使用EditorCurveBinding. 将关键帧和图像的Sprite属性进行关联
        EditorCurveBinding editorCurveBinding = new EditorCurveBinding();
        editorCurveBinding.type = typeof(Image);
        editorCurveBinding.path = "";
        editorCurveBinding.propertyName = "m_Sprite";

        //创建关键帧.数量为所选中的精灵数量.为关键帧分配精灵
        ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[selectedSprites.Count];

        for(int i= 0; i < selectedSprites.Count; i++)
        {
            keyFrames[i] = new ObjectReferenceKeyframe();
            keyFrames[i].time = i * defaultInterval;
            keyFrames[i].value = selectedSprites[i];
        }

        AnimationUtility.SetObjectReferenceCurve(animationClip, editorCurveBinding, keyFrames);

        //由于Loop Time属性无法直接从脚本中进行设置
        //因此使用SerializedProperty进行设置
        //该方法在Unity将来的版本中可能无法使用
        SerializedObject serializedAnimationClip = new SerializedObject(animationClip);
        SerializedProperty serializedAnimationClipSettings = serializedAnimationClip.FindProperty("m_AnimationClipSettings");
        serializedAnimationClipSettings.FindPropertyRelative("m_LoopTime").boolValue = true;
        serializedAnimationClip.ApplyModifiedProperties();

        //将创建的动画剪辑作为资源保存
        SaveAsset(animationClip, baseDir + "/" + baseName + ".anim");

        //创建动画控制器
        AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPathWithClip(baseDir + "/" + baseName + ".controller" , animationClip);
        animator.runtimeAnimatorController = (RuntimeAnimatorController)animatorController;
        
    }

    private static void SaveAsset(Object obj , string path)
    {
        Object existingAsset = AssetDatabase.LoadMainAssetAtPath(path);
        if(existingAsset != null)
        {
            EditorUtility.CopySerialized(obj, existingAsset);
            AssetDatabase.SaveAssets();
        }
        else
        {
            AssetDatabase.CreateAsset(obj, path);
        }
    }
}
