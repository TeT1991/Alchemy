using System.Collections.Generic;

public class ElementCombiner
{
    private readonly RecipeDatabase _recipeDatabase;
    private readonly ElementCollection _elementCollection;

    public ElementCombiner(RecipeDatabase recipeDatabase, ElementCollection elementCollection)
    {
        _recipeDatabase = recipeDatabase;
        _elementCollection = elementCollection;
    }

    public CombinationResult TryCombine(List<ElementData> ingredients)
    {
        var recipe = _recipeDatabase.FindMatch(ingredients.ToArray());

        if (recipe == null)
            return new CombinationResult(CombinationResultStatus.NoMatch);

        if (_elementCollection.IsDiscovered(recipe.Result))
        {
            var existingElement = _elementCollection.GetElementById(recipe.Result.Id);
            return new CombinationResult(CombinationResultStatus.AlreadyDiscovered, existingElement);
        }

        _elementCollection.TryAdd(recipe.Result, out var newElement);
        return new CombinationResult(CombinationResultStatus.SuccessNewElement, newElement);
    }
}
