using UnityEditor;

[CustomEditor(typeof(Modifier))]
// True -> the child classes of Modifier inherit also the inspector visualization.
public class ModifierEditor : Editor
{
    #region Properties
    public Modifier modifier_Script;
    SerializedProperty duration;

    SerializedProperty loop;
    SerializedProperty loopCounter;
    SerializedProperty loopEnd;

    public SerializedProperty autoStart;
    public SerializedProperty startDelay;

    SerializedProperty use_OutCurve;
    
    bool loopMenu = false;
    bool curveMenu = false;
    public bool baseSettings = false;
    public bool startMenu = false;
    public bool stopMenu = false;
    #endregion
    public virtual void OnEnable()
    {
        modifier_Script = (Modifier)target;
        duration = serializedObject.FindProperty("duration");

        loop = serializedObject.FindProperty("loop");
        loopCounter = serializedObject.FindProperty("loopCounter");
        loopEnd = serializedObject.FindProperty("loopEnd");

        autoStart = serializedObject.FindProperty("autoStart");
        startDelay = serializedObject.FindProperty("startDelay");

        use_OutCurve = serializedObject.FindProperty("use_OutCurve");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(duration);

        loopMenu = EditorGUILayout.BeginFoldoutHeaderGroup(loopMenu, "Loop");
        if (loopMenu)
        {
            EditorGUILayout.PropertyField(loop);
            if(modifier_Script.loop)
                EditorGUILayout.PropertyField(loopCounter);
            EditorGUILayout.PropertyField(loopEnd);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        curveMenu = EditorGUILayout.BeginFoldoutHeaderGroup(curveMenu, "Curve");
        if (curveMenu)
        {
            EditorGUILayout.LabelField("In Curve");
            EditorGUILayout.CurveField(modifier_Script.inCurve);
            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(use_OutCurve);
            if (modifier_Script.use_OutCurve)
            {
                EditorGUILayout.LabelField("Out Curve");
                EditorGUILayout.CurveField(modifier_Script.outCurve);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();
    }
}
