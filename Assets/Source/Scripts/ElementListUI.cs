using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementListUI : MonoBehaviour
{
    [SerializeField] private Transform _contentParent;
    [SerializeField] private ElementListItem _itemPrefab;
    [SerializeField] private Image _progressFill; 
    [SerializeField] private TextMeshProUGUI _progressText;

    private ElementCollection _collection;
    private List<ElementListItem> _items = new();
    private Action<Element> _onElementSelected;

    public void Initialize(ElementCollection collection, Action<Element> onElementSelected)
    {
        _collection = collection;
        _onElementSelected = onElementSelected;

        _collection.OnElementDiscovered += HandleElementDiscovered;

        RebuildList();
        UpdateProgressBar(); 
    }

    private void OnDestroy()
    {
        if (_collection != null)
            _collection.OnElementDiscovered -= HandleElementDiscovered;
    }

    private void RebuildList()
    {
        foreach (Transform child in _contentParent)
            Destroy(child.gameObject);
        _items.Clear();

        foreach (var element in _collection.AllDiscovered)
            CreateItem(element);
    }

    private void HandleElementDiscovered(Element element)
    {
        CreateItem(element);
    }

    private void CreateItem(Element element)
    {
        var item = Instantiate(_itemPrefab, _contentParent);
        item.Initialize(element, () => _onElementSelected?.Invoke(element));
        _items.Add(item);

        _collection.MarkAsSeen(element.Data);
    }

    public void RefreshElementUI(Element element)
    {
        foreach (var item in _items)
        {
            if (item.Element == element)
            {
                item.Refresh();
                break;
            }
        }
    }

    public void UpdateProgressBar()
    {
        if (_collection == null) return;

        int discovered = _collection.AllDiscovered.Count;
        int total = _collection.TotalCount;
        float progress = (float)discovered / total;

        _progressFill.fillAmount = progress;
        _progressText.text = $"{discovered} / {total}";
    }
}

