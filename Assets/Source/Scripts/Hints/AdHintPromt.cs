using UnityEngine;
using UnityEngine.UI;

public class AdHintPrompt : BasePanel
{
    [SerializeField] private HintInventory _inventory;
    [SerializeField] private Button _confirmButton;

    private void Start()
    {
        _confirmButton.onClick.AddListener(() =>
        {
            _inventory.Add(1);
            Hide();
        });
    }
}

