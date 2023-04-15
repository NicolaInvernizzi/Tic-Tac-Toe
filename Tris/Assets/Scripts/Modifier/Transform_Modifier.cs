using UnityEngine;

public class Transform_Modifier : Modifier
{
    #region Variables
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

    // if autoStart = false -> realStart = start 
    private Vector3 realStart;
    private Vector3 currentStart;
    private Vector3 currentStop;
    #endregion

    #region Flow
    void Start()
    {
        ResetStartingValues();
        if (gameObject.GetComponent<Transform>() != null)
            realStart = GetTransformInfo(operation, autoStart, transform, start);
        else
        {
            realStart = start;
            Debug.LogWarning("There's no transform");
        }
           
    }
    private void Update()
    {
        if (gameObject.GetComponent<Transform>() != null && Delay())
        {
            Timer();
            Modifier();
        }       
    }
    #endregion

    #region Methods
    private void Modifier()
    {
        float xValue = transform.position.x;
        float yValue = transform.position.y;
        float zValue = transform.position.z;
        Vector3 value;

        currentStart = GetTransformInfo(operation, useTransform_Start, start_Transform, realStart);
        currentStop = GetTransformInfo(operation, useTransform_Stop, stop_Transform, stop);

        value = Lerp(currentStart, currentStop);

        if (x)
            xValue = value.x;
        if (y)
            yValue = value.y;
        if (z)
            zValue = value.z;

        AssignVector3(operation, transform, new Vector3(xValue, yValue, zValue));
    }
    /// <summary>
    /// Assign Vector3 v to Transform t property (position, rotation, scale) based on operation o (enum) value.
    /// </summary>
    private void AssignVector3(TransformOperation o, Transform t, Vector3 v)
    {
        switch (o)
        {
            case TransformOperation.POSITION:
                t.position = v; break;
            case TransformOperation.LOCAL_POSITION:
                t.localPosition = v; break;
            case TransformOperation.ROTATION:
                t.rotation = Quaternion.Euler(v); break;
            case TransformOperation.LOCAL_ROTATION:
                t.localRotation = Quaternion.Euler(v); break;
            case TransformOperation.SCALE:
                t.localScale = v; break;
        }
    }
    /// <summary>
    /// Return Transform property (position, rotation, scale) based on operation o (enum) value.
    /// if the bool b is false return Vector3 v.
    /// </summary>
    private Vector3 GetTransformInfo(TransformOperation o, bool b, Transform t, Vector3 v)
    {
        switch (b)
        {
            case true:
                switch (t)
                {
                    case null:
                        Debug.LogWarning("There's no Transform");
                        return v;
                    default:
                        switch (o)
                        {
                            case TransformOperation.POSITION:
                                return t.position;
                            case TransformOperation.LOCAL_POSITION:
                                return t.localPosition;
                            case TransformOperation.ROTATION:
                                return t.rotation.eulerAngles;
                            case TransformOperation.LOCAL_ROTATION:
                                return t.localRotation.eulerAngles;
                            default:
                                return t.localScale;
                        }
                }
            default:
                return v;

        }
    }
    #endregion
}
public enum TransformOperation
{
    POSITION,
    LOCAL_POSITION,
    ROTATION,
    LOCAL_ROTATION,
    SCALE
}
