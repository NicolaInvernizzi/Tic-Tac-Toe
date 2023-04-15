using UnityEngine;

public static class ScriptableManager
{
    public static Transform_Modifier Transform_ModifierSetUp(ScriptableTransform_Modifier data, GameObject gameObject)
    {
        Transform_Modifier transform_Modifier = gameObject.AddComponent<Transform_Modifier>();
        ModifierSetUp(data, transform_Modifier);
        transform_Modifier.operation = data.operation;
        transform_Modifier.useTransform_Start = data.useTransform_Start;
        transform_Modifier.useTransform_Stop = data.useTransform_Stop;
        transform_Modifier.start = data.start;
        transform_Modifier.stop = data.stop;
        transform_Modifier.start_Transform = data.start_Transform;
        transform_Modifier.stop_Transform = data.stop_Transform;
        transform_Modifier.x = data.x;
        transform_Modifier.y = data.y;
        transform_Modifier.z = data.z;
        return transform_Modifier;
    }
    public static Image_Modifier Image_ModifierSetUp(ScriptableImage_Modifier data, GameObject gameObject)
    {
        Image_Modifier image_Modifier = gameObject.AddComponent<Image_Modifier>();
        ModifierSetUp(data, image_Modifier);
        image_Modifier.operation = data.operation;
        image_Modifier.useImage_Start = data.useImage_Start;
        image_Modifier.useImage_Stop = data.useImage_Stop;
        image_Modifier.start = data.start;
        image_Modifier.stop = data.stop;
        image_Modifier.start_Image = data.start_Image;
        image_Modifier.stop_Image = data.stop_Image;
        return image_Modifier;
    }

    private static void ModifierSetUp(ScriptableModifier data, Modifier modifier)
    {
        modifier.startDelay = data.startDelay;
        modifier.duration = data.duration;
        modifier.loop = data.loop;
        modifier.loopCounter = data.loopCounter;
        modifier.loopEnd = data.loopEnd;
        modifier.autoStart = data.autoStart;
        modifier.use_OutCurve = data.use_OutCurve;
        modifier.inCurve = data.inCurve;
        modifier.outCurve = data.outCurve;
    }
}
