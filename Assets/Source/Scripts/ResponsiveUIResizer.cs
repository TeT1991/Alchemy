using System.Collections.Generic;
using UnityEngine;

public class ResponsiveUIResizer : MonoBehaviour
{
    [Header("�������� ���������")]
    [SerializeField] private RectTransform _target;

    [Header("��������� ��������")]
    [Tooltip("������ � ���������� ���������� (��������� ������).")]
    [SerializeField] private float defaultPortraitHeight = 860f;

    [Tooltip("������ � �������������� ���������� (��������, ��������).")]
    [SerializeField] private float defaultLandscapeHeight = 450f;

    [Header("�������������� ������� ��� ������ ����������� ������")]
    [Tooltip("��������� ��� ������������� ������� (������� �������, ������������ �������� � �.�.)")]
    [SerializeField] private List<ScreenSizeSetting> _settings = new();

    private void Start()
    {
        ApplyResize();
    }

    private void ApplyResize()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        float chosenHeight = IsPortrait() ? defaultPortraitHeight : defaultLandscapeHeight;

        foreach (var setting in _settings)
        {
            if (aspectRatio >= setting.AspectRatioThreshold)
            {
                chosenHeight = setting.TargetHeight;
            }
        }

        _target.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, chosenHeight);
    }

    private bool IsPortrait()
    {
        return Screen.height >= Screen.width;
    }
}