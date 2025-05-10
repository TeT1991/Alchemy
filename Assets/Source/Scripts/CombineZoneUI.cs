using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombineZoneUI : MonoBehaviour
{
    [Header("Слоты элементов")]
    [SerializeField] private List<Image> _slotIcons;
    [SerializeField] private List<TextMeshProUGUI> _slotLabels;

    [Header("Кнопки и текст")]
    [SerializeField] private Button _combineButton;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private Button _clearButton;

    [Header("Заглушка")]
    [SerializeField] private Sprite _emptyIcon;

    private List<Element> _selectedElements = new();
    private ElementCombiner _combiner;
    private ElementCollection _elementCollection;
    private ElementListUI _elementListUI;

    public IReadOnlyList<Element> SelectedElements => _selectedElements;

    public void Initialize(ElementCombiner combiner, ElementCollection elementCollection, ElementListUI elementListUI)
    {
        _combiner = combiner;
        _elementCollection = elementCollection;
        _elementListUI = elementListUI;

        _combineButton.onClick.AddListener(OnCombineClicked);
        _clearButton.onClick.AddListener(ClearSelection);

        ClearUI();
    }

    public void SetElement(Element element)
    {
        if (_selectedElements.Contains(element))
            return;

        if (_selectedElements.Count >= _slotIcons.Count)
        {
            ShowMessage("Максимум 3 элемента. Очистите зону или нажмите 'Смешать'.");
            return;
        }

        _selectedElements.Add(element);
        UpdateSlots();
    }

    public void SetHintElements(params Element[] elements)
    {
        _selectedElements.Clear();
        _selectedElements.AddRange(elements);
        UpdateSlots();
    }

    private void OnCombineClicked()
    {
        if (_selectedElements.Count < 2)
        {
            ShowMessage("Нужно минимум 2 элемента.");
            return;
        }

        var dataList = new List<ElementData>();
        foreach (var e in _selectedElements)
            dataList.Add(e.Data);

        var result = _combiner.TryCombine(dataList);

        switch (result.Status)
        {
            case CombinationResultStatus.NoMatch:
                ShowMessage("Ничего не получилось...");
                break;

            case CombinationResultStatus.AlreadyDiscovered:
                ShowMessage($"Уже открыт: {result.NewElement.Data.DisplayName}");
                break;

            case CombinationResultStatus.SuccessNewElement:
                ShowMessage($"Новый элемент: {result.NewElement.Data.DisplayName}!");
                break;
        }

        foreach (var element in _selectedElements)
        {
            _elementCollection.MarkAsSeen(element.Data);
            _elementListUI.RefreshElementUI(element);
        }

        ClearSelection();
    }

    private void UpdateSlots()
    {
        for (int i = 0; i < _slotIcons.Count; i++)
        {
            if (i < _selectedElements.Count)
            {
                var element = _selectedElements[i];
                _slotIcons[i].sprite = element.Data.Icon;
                _slotIcons[i].color = Color.white;
                _slotLabels[i].text = element.Data.DisplayName;
            }
            else
            {
                _slotIcons[i].sprite = _emptyIcon;
                _slotIcons[i].color = new Color(1f, 1f, 1f, 0f);
                _slotLabels[i].text = "";
            }
        }

        _combineButton.interactable = _selectedElements.Count >= 2;
    }

    private void ClearSelection()
    {
        _selectedElements.Clear();
        UpdateSlots();
    }

    private void ClearUI()
    {
        _selectedElements.Clear();
        ShowMessage("");
        UpdateSlots();
    }

    public void ShowMessage(string message)
    {
        _resultText.text = message;
    }
}
