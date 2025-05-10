using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private RecipeDatabase _recipeDatabase;
    [SerializeField] private ElementListUI _elementListUI;
    [SerializeField] private CombineZoneUI _combineZoneUI;
    [SerializeField] private ElementData[] _allElements;

    [SerializeField] private HintInventory _hintInventory;
    [SerializeField] private HintProvider _hintProvider;
    [SerializeField] private HintTrigger _hintTrigger;
    [SerializeField] private HintUI _hintUI;
    [SerializeField] private AdHintPrompt _adHintPrompt;

    private ElementCollection _elementCollection;
    private ElementCombiner _combiner;

    private void Start()
    {
        _elementCollection = new ElementCollection(_allElements);
        _combiner = new ElementCombiner(_recipeDatabase, _elementCollection);

        _hintProvider.SetCollection(_elementCollection);

        _elementListUI.Initialize(_elementCollection, OnElementSelected);
        _combineZoneUI.Initialize(_combiner, _elementCollection, _elementListUI);

        _hintUI.Initialize(_hintInventory, _hintProvider);

        _hintTrigger.OnAdHintAvailable += _adHintPrompt.Show;
    }

    private void OnElementSelected(Element selected)
    {
        _combineZoneUI.SetElement(selected);
    }
}
