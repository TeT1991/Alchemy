using UnityEngine;

[System.Serializable]
public class ScreenSizeSetting
{
    [Tooltip("����������� ����������� ������ (width/height), ��� ������� ����������� ��� �������.")]
    public float AspectRatioThreshold = 1.7f;

    [Tooltip("������� ������ ������ ��� ���� �����������.")]
    public float TargetHeight = 400f;

    [Tooltip("������� ������ ������ ��� ���� �����������.")]
    public float TargetWidth = 400f;
}