using UnityEngine;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    [SerializeField] private Button _closeButton;

    protected virtual void Awake()
    {
        if (_closeButton != null)
            _closeButton.onClick.AddListener(Hide);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
