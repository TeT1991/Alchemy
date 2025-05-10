using UnityEngine;
using System;

public class HintInventory : MonoBehaviour
{
    private const string PlayerPrefsKey = "hint_count";

    private int _count;

    public int Count => _count;

    public event Action<int> OnCountChanged;

    private void Awake()
    {
        _count = PlayerPrefs.GetInt(PlayerPrefsKey, 0);
    }

    public void Add(int amount)
    {
        _count += amount;
        Save();
        OnCountChanged?.Invoke(_count);
    }

    public bool TryUse()
    {
        if (_count <= 0)
            return false;

        _count--;
        Save();
        OnCountChanged?.Invoke(_count);
        return true;
    }

    private void Save()
    {
        PlayerPrefs.SetInt(PlayerPrefsKey, _count);
        PlayerPrefs.Save();
    }
}
