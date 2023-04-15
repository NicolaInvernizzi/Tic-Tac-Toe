using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(Image_Modifier))]
public class ImageModifierEditor : ModifierEditor
{
    private Image_Modifier image_ModScript;
    SerializedProperty operation;
    SerializedProperty useImage_Start;
    SerializedProperty useImage_Stop;
    SerializedProperty start;
    SerializedProperty stop;
    SerializedProperty start_Image;
    SerializedProperty stop_Image;

    public override void OnEnable()
    {
        image_ModScript = (Image_Modifier)target;
        base.OnEnable();
        operation = serializedObject.FindProperty("operation");
        useImage_Start = serializedObject.FindProperty("useImage_Start");
        useImage_Stop = serializedObject.FindProperty("useImage_Stop");
        start = serializedObject.FindProperty("start");
        stop = serializedObject.FindProperty("stop");
        start_Image = serializedObject.FindProperty("start_Image");
        stop_Image = serializedObject.FindProperty("stop_Image");
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
        if (startMenu)
        {
            EditorGUILayout.PropertyField(autoStart);
            if (!modifier_Script.autoStart)
            {
                EditorGUILayout.PropertyField(useImage_Start);
                if (image_ModScript.useImage_Start)
                    EditorGUILayout.PropertyField(start_Image);
                else
                    EditorGUILayout.PropertyField(start);
            }
            EditorGUILayout.PropertyField(startDelay);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        stopMenu = EditorGUILayout.BeginFoldoutHeaderGroup(stopMenu, "STOP");
        if (stopMenu)
        {

            EditorGUILayout.PropertyField(useImage_Stop);
            if (image_ModScript.useImage_Stop)
                EditorGUILayout.PropertyField(stop_Image);
            else
                EditorGUILayout.PropertyField(stop);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();
    }
}
