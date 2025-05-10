using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeDatabase", menuName = "Alchemy/Recipe Database")]
public class RecipeDatabase : ScriptableObject
{
    [SerializeField] private List<Recipe> _recipes;

    public IReadOnlyList<Recipe> Recipes => _recipes;

    // Находит подходящий рецепт для переданных элементов
    public Recipe FindMatch(params ElementData[] elements)
    {
        foreach (var recipe in _recipes)
        {
            if (recipe.Matches(elements))
                return recipe;
        }

        return null;
    }

    // Возвращает все рецепты, которые дают указанный результат
    public List<Recipe> GetRecipesWithResult(ElementData result)
    {
        return _recipes.FindAll(r => r.Result == result);
    }
}
