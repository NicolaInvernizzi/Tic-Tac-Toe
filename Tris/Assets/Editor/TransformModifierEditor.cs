using UnityEditor;
using UnityEngine;
// So in the child Modifier editor script i can create a specific editor for the child while using the parent
// OnInspectorGUI (base is no longer the unity default one)
[CustomEditor(typeof(Transform_Modifier))]
public class TransformModifierEditor : ModifierEditor 
{
    private Transform_Modifier transform_ModScript;
    SerializedProperty operation;
    SerializedProperty useTransform_Start;
    SerializedProperty useTransform_Stop;
    SerializedProperty start;
    SerializedProperty stop;
    SerializedProperty start_Transform;
    SerializedProperty stop_Transform;
    SerializedProperty x;
    SerializedProperty y;
    SerializedProperty z;
    bool axisMenu = false;

    //bool stopMenu = false;
    public override void OnEnable()
    {
        transform_ModScript = (Transform_Modifier)target;
        base.OnEnable();
        operation = serializedObject.FindProperty("operation");
        useTransform_Start = serializedObject.FindProperty("useTransform_Start");
        useTransform_Stop = serializedObject.FindProperty("useTransform_Stop");
        start = serializedObject.FindProperty("start");
        stop = serializedObject.FindProperty("stop");
        start_Transform = serializedObject.FindProperty("start_Transform");
        stop_Transform = serializedObject.FindProperty("stop_Transform");
        x = serializedObject.FindProperty("x");
        y = serializedObject.FindProperty("y");
        z = serializedObject.FindProperty("z");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(operation);

        baseSettings = EditorGUILayout.BeginFoldoutHeaderGroup(baseSettings, "BASE SETTINGS");
        EditorGUILayout.EndFoldoutHeaderGroup();
        if (baseSettings)
            base.OnInspectorGUI();

        startMenu = EditorGUILayout.BeginFoldoutHeaderGroup(startMenu, "START");
        if(startMenu)
        {
            EditorGUILayout.PropertyField(autoStart);
            if(!modifier_Script.autoStart)
            {
                EditorGUILayout.PropertyField(useTransform_Start);
                if (transform_ModScript.useTransform_Start)
                    EditorGUILayout.PropertyField(start_Transform);
                else
                    EditorGUILayout.PropertyField(start);
            }
            EditorGUILayout.PropertyField(startDelay);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        stopMenu = EditorGUILayout.BeginFoldoutHeaderGroup(stopMenu, "STOP");
        if (stopMenu)
        {

            EditorGUILayout.PropertyField(useTransform_Stop);
            if(transform_ModScript.useTransform_Stop)
                EditorGUILayout.PropertyField(stop_Transform);
            else
                EditorGUILayout.PropertyField(stop);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        axisMenu = EditorGUILayout.BeginFoldoutHeaderGroup(axisMenu, "AXIS");
        if (axisMenu)
        {
            EditorGUILayout.PropertyField(x);
            EditorGUILayout.PropertyField(y);
            EditorGUILayout.PropertyField(z);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();
    }
}
