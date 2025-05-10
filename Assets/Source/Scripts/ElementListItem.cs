using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementListItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _newIndicator;
    [SerializeField] private Sprite _defaultIcon;

    private Element _element;

    public Element Element => _element;

    public void Initialize(Element element, Action onClick)
    {
        _element = element;
        _icon.sprite = element.Data.Icon != null ? element.Data.Icon : _defaultIcon;

        Color discoveredColor = Color.white;
        Color unDiscoveredColor = Color.black;
        string unDiscoverdName = "?????";

        if (element.IsDiscovered)
        {
            SetUpItem(discoveredColor, element.Data.DisplayName, element.IsNew, true, onClick);
            element.MarkAsSeen();
        }
        else
        {
            SetUpItem(unDiscoveredColor, unDiscoverdName, false, false);
        }
    }

    public void SetUpItem(Color color, string displayName, bool isNew, bool interactable, Action onClick = null)
    {
        _icon.color = color;
        _label.text = displayName;
        _newIndicator.SetActive(isNew);

        _button.interactable = interactable;
        _button.onClick.RemoveAllListeners();

        if (interactable && onClick != null)
            _button.onClick.AddListener(() => onClick.Invoke());
    }

    public void Refresh()
    {
        _newIndicator.SetActive(_element.IsNew);
    }
}
