using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HintProvider : MonoBehaviour
{
    [SerializeField] private RecipeDatabase _recipes;
    private ElementCollection _collection;

    public void SetCollection(ElementCollection collection)
    {
        _collection = collection;
    }

    public bool TryGetAnyHint(out Element[] elements)
    {
        foreach (var recipe in _recipes.Recipes)
        {
            if (_collection.IsDiscovered(recipe.Result))
                continue;

            if (recipe.Ingredients.All(i => _collection.IsDiscovered(i)))
            {
                elements = recipe.Ingredients
                    .Select(i => _collection.GetElementById(i.Id))
                    .ToArray();
                return true;
            }
        }

        elements = null;
        return false;
    }

    public bool TryGetHintWith(Element known, out Element[] otherElements)
    {
        foreach (var recipe in _recipes.Recipes)
        {
            if (_collection.IsDiscovered(recipe.Result))
                continue;

            if (!recipe.Ingredients.Contains(known.Data))
                continue;

            var result = new List<Element>();
            foreach (var ingredient in recipe.Ingredients)
            {
                if (ingredient == known.Data) continue;
                if (!_collection.IsDiscovered(ingredient))
                {
                    otherElements = null;
                    return false;
                }

                result.Add(_collection.GetElementById(ingredient.Id));
            }

            otherElements = result.ToArray();
            return true;
        }

        otherElements = null;
        return false;
    }
}
