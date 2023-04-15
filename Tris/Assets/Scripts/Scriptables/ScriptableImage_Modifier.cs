using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "newImage_Modifier", menuName = "ScriptableObjects/Modifier/Image")]
public class ScriptableImage_Modifier : ScriptableModifier
{
    public ColorOperation operation;
    [Tooltip("Override start with start_image operation values")]
    public bool useImage_Start;
    [Tooltip("Override stop with stop_image operation values")]
    public bool useImage_Stop;
    [Range(0, 1)] public float start;
    [Range(0, 1)] public float stop;
    public Image start_Image;
    public Image stop_Image;
}
