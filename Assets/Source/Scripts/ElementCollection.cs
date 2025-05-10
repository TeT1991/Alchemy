using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ElementCollection
{
    private readonly Dictionary<string, Element> _discoveredElements = new();
    private readonly List<ElementData> _allElements;

    public IReadOnlyCollection<Element> AllDiscovered => _discoveredElements.Values;
    public int TotalCount => _allElements.Count;

    public event Action<Element> OnElementDiscovered;

    public ElementCollection(IEnumerable<ElementData> allElements)
    {
        _allElements = new List<ElementData>(allElements);

        foreach (var elementData in _allElements)
        {
            if (IsElementSavedAsDiscovered(elementData.Id))
            {
                var element = new Element(elementData);
                element.MarkAsDiscovered();

                if (IsElementMarkedAsNew(elementData.Id))
                    element.MarkAsDiscovered();
                else
                    element.MarkAsSeen();

                _discoveredElements[elementData.Id] = element;
            }
            else if (elementData.IsStartingElement)
            {
                var element = new Element(elementData);
                element.MarkAsDiscovered();
                _discoveredElements[elementData.Id] = element;

                SaveDiscoveredElement(elementData.Id);
                SaveNewElementFlag(elementData.Id, true); 
            }
        }
    }


    public bool IsDiscovered(ElementData data)
    {
        return _discoveredElements.ContainsKey(data.Id);
    }

    public bool TryAdd(ElementData data, out Element newElement)
    {
        if (IsDiscovered(data))
        {
            newElement = null;
            return false;
        }

        newElement = new Element(data);
        newElement.MarkAsDiscovered();
        _discoveredElements[data.Id] = newElement;

        SaveDiscoveredElement(data.Id);
        SaveNewElementFlag(data.Id, true);

        OnElementDiscovered?.Invoke(newElement);
        return true;
    }

    public void MarkAsSeen(ElementData data)
    {
        if (_discoveredElements.TryGetValue(data.Id, out var element))
        {
            element.MarkAsSeen();
            SaveNewElementFlag(data.Id, false);
        }
    }

    public Element GetElementById(string id)
    {
        _discoveredElements.TryGetValue(id, out var element);
        return element;
    }

    private void SaveDiscoveredElement(string id)
    {
        PlayerPrefs.SetInt(GetDiscoveredKey(id), 1);
    }

    private bool IsElementSavedAsDiscovered(string id)
    {
        return PlayerPrefs.GetInt(GetDiscoveredKey(id), 0) == 1;
    }

    private void SaveNewElementFlag(string id, bool isNew)
    {
        PlayerPrefs.SetInt(GetNewKey(id), isNew ? 1 : 0);
    }

    private bool IsElementMarkedAsNew(string id)
    {
        return PlayerPrefs.GetInt(GetNewKey(id), 0) == 1;
    }

    private string GetDiscoveredKey(string id) => $"element_discovered_{id}";
    private string GetNewKey(string id) => $"element_new_{id}";
}
