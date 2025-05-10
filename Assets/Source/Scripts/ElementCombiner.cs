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

    public CombinationResult TryCombine(List<ElementData> inputElements)
    {
        var recipe = _recipeDatabase.FindMatch(inputElements);

        if (recipe == null)
            return new CombinationResult(CombinationResultStatus.NoMatch);

        if (_elementCollection.IsDiscovered(recipe.Result))
        {
            var existing = _elementCollection.GetElementById(recipe.Result.Id);
            return new CombinationResult(CombinationResultStatus.AlreadyDiscovered, existing);
        }

        _elementCollection.TryAdd(recipe.Result, out var newElement);
        return new CombinationResult(CombinationResultStatus.SuccessNewElement, newElement);
    }
}
