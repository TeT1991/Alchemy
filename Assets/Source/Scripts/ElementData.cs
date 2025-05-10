using UnityEngine;

[CreateAssetMenu(fileName = "NewElement", menuName = "Alchemy/Element")]
public class ElementData : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _displayName;
    [SerializeField] private Sprite _icon;

    [SerializeField] private bool _isStartingElement = false;

    public string Id => _id;
    public string DisplayName => _displayName;
    public Sprite Icon => _icon;
    public bool IsStartingElement => _isStartingElement;
}