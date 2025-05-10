using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    [SerializeField] private List<ElementData> _ingredients;
    [SerializeField] private ElementData _result;

    public IReadOnlyList<ElementData> Ingredients => _ingredients;
    public ElementData Result => _result;

    // Сравнивает два набора элементов (без учёта порядка)
    public bool Matches(params ElementData[] elements)
    {
        if (elements.Length != _ingredients.Count) return false;

        var inputSet = new HashSet<ElementData>(elements);
        var recipeSet = new HashSet<ElementData>(_ingredients);

        return inputSet.SetEquals(recipeSet);
    }

    public bool ContainsIngredient(ElementData data)
    {
        return _ingredients.Contains(data);
    }
}
