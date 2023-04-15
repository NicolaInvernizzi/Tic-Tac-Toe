using UnityEditor;
using UnityEngine;

public class Modifier : MonoBehaviour
{
    #region Variables
    [Range(0.1f, 60)] public float duration;
    public bool loop;

    [Tooltip("How many times loop before loopEnd")]
    [Range(1, 100)] public int loopCounter;

    [SerializeField] public LoopEnd loopEnd;

    [Tooltip("Set start value to default Operation value")]
    public bool autoStart;

    [Tooltip("Wait startDelay seconds befor apllying animation")]
    [Range(0, 10)] public float startDelay;

    [Tooltip("Use two different curves")]
    public bool use_OutCurve;

    [Tooltip("From start to stop")]
    public AnimationCurve inCurve;

    [Tooltip("From stop to start")]
    public AnimationCurve outCurve;

    private int invert;
    private int loopC;
    private float timer;
    private bool delay;
    #endregion

    #region Methods
    protected float Lerp(float start, float stop)
    {
        float result;
        switch (invert)
        {
            case -1:
                if(!use_OutCurve)
                    result = Mathf.Lerp(stop, start, inCurve.Evaluate(timer / duration));
                else
                    result = Mathf.Lerp(stop, start, outCurve.Evaluate(timer / duration));
                if (result == start)
                    Invert();
                break;
            default:
                result = Mathf.Lerp(start, stop, inCurve.Evaluate(timer / duration));
                if (result == stop)
                    Invert();
                break;
        }              
        return result;
    }
    protected Vector3 Lerp(Vector3 start, Vector3 stop)
    {
        Vector3 result;
        switch (invert)
        {
            case -1:
                if(!use_OutCurve)
                    result = Vector3.Lerp(stop, start, inCurve.Evaluate(timer / duration));
                else
                    result = Vector3.Lerp(stop, start, outCurve.Evaluate(timer / duration));
                if (result == start)
                    Invert();
                break;
            default:
                result = Vector3.Lerp(start, stop, inCurve.Evaluate(timer / duration));
                if (result == stop)
                    Invert();
                break;
        }
        return result;
    }
    private void Invert()
    {
        timer = 0;
        invert *= -1;

        if (loop && loopC < loopCounter - 1)
            loopC++;
        else
        {
            switch (loopEnd)
            {
                case LoopEnd.DISABLE:
                    this.enabled = false;
                    break;
                case LoopEnd.DESTROY:
                    Destroy(this);
                    break;
            }
        }
    }
    protected void Timer()
    {
        timer += Time.deltaTime;
    }
    protected bool Delay()
    {
        if(delay)
        {
            if (startDelay > 0)
            {
                timer += Time.deltaTime;
                if (timer >= startDelay)
                {
                    timer = 0;
                    delay = false;
                }
            }
            else
            {
                delay = false;
                return true;
            }
        }
        else
            return true;
        return false;
    }
    protected void ResetStartingValues()
    {
        invert = 1;
        loopC = 0;
        timer = 0;
    }
    #endregion
}
public enum LoopEnd
{
    DISABLE,
    DESTROY
}
