using UnityEngine;
using UnityEngine.UI;

public class Image_Modifier : Modifier
{
    #region Variables
    public ColorOperation operation;
    [Tooltip("Override start with start_image operation values")]
    public bool useImage_Start;
    [Tooltip("Override stop with stop_image operation values")]
    public bool useImage_Stop;
    [Range(0, 1)] public float start;
    [Range(0, 1)] public float stop;
    public Image start_Image;
    public Image stop_Image;

    private Image image_Component;
    private Color image_Color;

    // if autoStart = false -> realStart = start 
    private float realStart;
    private float currentStart;
    private float currentStop;
    #endregion

    #region Flow
    void Start()
    {
        ResetStartingValues();
        if (gameObject.GetComponent<Image>() != null)
        {
            image_Component = GetComponent<Image>();
            image_Color = image_Component.color;
            realStart = GetImageInfo(operation, autoStart, image_Component, start);
        }         
        else
        {
            realStart = start;
            Debug.LogWarning("There's no Image");
        }
    }
    void Update()
    {
        if (gameObject.GetComponent<Image>() != null && Delay())
        {
            Timer();
            Modifier();
        }
    }
    #endregion

    #region Mehods
    private void Modifier()
    {
        currentStart = GetImageInfo(operation, useImage_Start, start_Image, realStart);
        currentStop = GetImageInfo(operation, useImage_Stop, stop_Image, stop);

        AssignFloat(operation, image_Color, Lerp(currentStart, currentStop));
    }
    /// <summary>
    /// Assign Float f to Color c channel (red, green, blue, alpha) based on operation o (enum) value.
    private void AssignFloat(ColorOperation o, Color c, float f)
    {
        switch (o)
        {
            case ColorOperation.RED:
                c.r = f; break;
            case ColorOperation.GREEN:
                c.g = f; break;
            case ColorOperation.BLUE:
                c.b = f; break;
            case ColorOperation.ALPHA:
                c.a = f; break;
            default:
                c *= new Color(f, f, f); break;
        }
        image_Component.color = c;
    }
    /// <summary>
    /// Return Image color channel (red, green, blue, alpha) based on operation o (enum) value.
    /// if the bool b is false return Vector3 v.
    /// </summary>
    private float GetImageInfo(ColorOperation o, bool b, Image i, float f)
    {
        switch(b)
        {
            case true:
                switch(i)
                {
                    case null:
                        Debug.LogWarning("There's no Image");
                        return f;
                    default:
                        switch (o)
                        {
                            case ColorOperation.RED:
                                return i.color.r;
                            case ColorOperation.GREEN:
                                return i.color.g;
                            case ColorOperation.BLUE:
                                return i.color.b;
                            default:
                                return i.color.a;
                        }
                }
            default:
                return f;

        }
    }
    #endregion
}
public enum ColorOperation
{
    RED,
    GREEN,
    BLUE,
    ALPHA,
    ADDITIVE
}
