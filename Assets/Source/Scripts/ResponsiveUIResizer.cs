using System.Collections.Generic;
using UnityEngine;

public class ResponsiveUIResizer : MonoBehaviour
{
    [Header("Основные настройки")]
    [SerializeField] private RectTransform _target;

    [Header("Дефолтные значения")]
    [Tooltip("Высота в портретной ориентации (мобильные экраны).")]
    [SerializeField] private float defaultPortraitHeight = 860f;

    [Tooltip("Высота в горизонтальной ориентации (десктопы, планшеты).")]
    [SerializeField] private float defaultLandscapeHeight = 450f;

    [Header("Индивидуальные правила для разных соотношений сторон")]
    [Tooltip("Настройки для нестандартных экранов (широкие дисплеи, суперширокие мониторы и т.п.)")]
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