using UnityEngine;

[System.Serializable]
public class ScreenSizeSetting
{
    [Tooltip("Минимальное соотношение сторон (width/height), при котором применяется это правило.")]
    public float AspectRatioThreshold = 1.7f;

    [Tooltip("Целевая высота панели при этом соотношении.")]
    public float TargetHeight = 400f;

    [Tooltip("Целевая ширина панели при этом соотношении.")]
    public float TargetWidth = 400f;
}