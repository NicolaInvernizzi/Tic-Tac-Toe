using UnityEngine;

public class ScriptableModifier : ScriptableObject
{
    [Range(0.1f, 60)] public float duration;
    public bool loop;
    [Tooltip("How many times loop before loopEnd")]
    [Range(1, 100)] public int loopCounter;
    [SerializeField] public LoopEnd loopEnd;
    [Tooltip("Set start value to default Operation value")]
    public bool autoStart;
    [Range(0, 10)] public float startDelay;
    [Tooltip("Use two different curves")]
    public bool use_OutCurve;
    [Tooltip("From start to stop")]
    public AnimationCurve inCurve;
    [Tooltip("From stop to start")]
    public AnimationCurve outCurve;
}
