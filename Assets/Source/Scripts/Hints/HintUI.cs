using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _counterText;
    [SerializeField] private Button _useHintButton;
    [SerializeField] private Button _openShopButton;

    [Header("Подключаемые компоненты")]
    [SerializeField] private HintShopPanel _shopPanel;
    [SerializeField] private CombineZoneUI _combineZoneUI;

    private HintInventory _inventory;
    private HintProvider _provider;

    public void Initialize(HintInventory inventory, HintProvider provider)
    {
        _inventory = inventory;
        _provider = provider;

        _inventory.OnCountChanged += UpdateUI;

        _useHintButton.onClick.AddListener(OnUseHint);
        _openShopButton.onClick.AddListener(() => _shopPanel.Show());

        _shopPanel.Initialize(_inventory);
        UpdateUI(_inventory.Count);
    }

    private void UpdateUI(int count)
    {
        _counterText.text = count.ToString();
    }

    private void OnUseHint()
    {
        if (!_inventory.TryUse())
        {
            _shopPanel.Show();
            return;
        }

        var selected = _combineZoneUI.SelectedElements;

        if (selected.Count == 0)
        {
            if (_provider.TryGetAnyHint(out var hintElements))
            {
                _combineZoneUI.SetHintElements(hintElements);
                ShowHintMessage(hintElements);
            }
            else
            {
                _combineZoneUI.ShowMessage("Все комбинации уже открыты!");
            }
        }
        else if (selected.Count == 1)
        {
            if (_provider.TryGetHintWith(selected[0], out var otherElements))
            {
                var full = new[] { selected[0] }.Concat(otherElements).ToArray();
                _combineZoneUI.SetHintElements(full);
                ShowHintMessage(full);
            }
            else
            {
                _combineZoneUI.ShowMessage("Нет доступных рецептов с этим элементом.");
            }
        }
        else
        {
            if (_provider.TryGetAnyHint(out var hintElements))
            {
                _combineZoneUI.SetHintElements(hintElements);
                ShowHintMessage(hintElements);
            }
            else
            {
                _combineZoneUI.ShowMessage("Все комбинации уже открыты!");
            }
        }
    }

    private void ShowHintMessage(Element[] elements)
    {
        string names = string.Join(" + ", elements.Select(e => e.Data.DisplayName));
        _combineZoneUI.ShowMessage($"Подсказка: попробуй {names}");
    }
}
