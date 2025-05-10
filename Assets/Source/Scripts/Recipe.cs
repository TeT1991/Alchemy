using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    [SerializeField] private List<ElementData> _ingredients;
    [SerializeField] private ElementData _result;

    public IReadOnlyList<ElementData> Ingredients => _ingredients;
    public ElementData Result => _result;

    public bool Matches(IReadOnlyList<ElementData> inputElements)
    {
        if (inputElements.Count != _ingredients.Count) return false;

        var inputSet = new HashSet<ElementData>(inputElements);
        var recipeSet = new HashSet<ElementData>(_ingredients);

        return inputSet.SetEquals(recipeSet);
    }

    public bool ContainsIngredient(ElementData element)
    {
        return _ingredients.Contains(element);
    }
}
