using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTransform_Modifier", menuName = "ScriptableObjects/Modifier/Transform")]
public class ScriptableTransform_Modifier : ScriptableModifier
{
    public TransformOperation operation;
    [Tooltip("Override start with start_transform operation values")]
    public bool useTransform_Start;
    [Tooltip("Override stop with stop_transform operation values")]
    public bool useTransform_Stop;
    public Vector3 start;
    public Vector3 stop;
    public Transform start_Transform;
    public Transform stop_Transform;
    public bool x;
    public bool y;
    public bool z;
}
