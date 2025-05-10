using UnityEngine;
using UnityEngine.UI;

public class HintShopPanel : BasePanel
{
    [SerializeField] private Button _buy1HintButton;
    [SerializeField] private Button _buy3HintButton;
    [SerializeField] private Button _buy5HintButton;

    private HintInventory _inventory;

    public void Initialize(HintInventory inventory)
    {
        _inventory = inventory;

        _buy1HintButton.onClick.AddListener(() => Buy(1));
        _buy3HintButton.onClick.AddListener(() => Buy(3));
        _buy5HintButton.onClick.AddListener(() => Buy(5));
    }

    private void Buy(int amount)
    {
        _inventory.Add(amount);
        Hide();
    }
}
