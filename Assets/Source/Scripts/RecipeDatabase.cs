using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeDatabase", menuName = "Alchemy/Recipe Database")]
public class RecipeDatabase : ScriptableObject
{
    [SerializeField] private List<Recipe> _recipes;

    public IReadOnlyList<Recipe> Recipes => _recipes;

    // ������� ���������� ������ ��� ���������� ���������
    public Recipe FindMatch(params ElementData[] elements)
    {
        foreach (var recipe in _recipes)
        {
            if (recipe.Matches(elements))
                return recipe;
        }

        return null;
    }

    // ���������� ��� �������, ������� ���� ��������� ���������
    public List<Recipe> GetRecipesWithResult(ElementData result)
    {
        return _recipes.FindAll(r => r.Result == result);
    }
}
