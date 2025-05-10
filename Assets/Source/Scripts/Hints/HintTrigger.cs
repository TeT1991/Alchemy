using System;
using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    [SerializeField] private int _failThreshold = 3;
    private int _failCount = 0;

    public event Action OnAdHintAvailable;

    public void RegisterFail()
    {
        _failCount++;
        if (_failCount >= _failThreshold)
        {
            _failCount = 0;
            OnAdHintAvailable?.Invoke();
        }
    }

    public void Reset() => _failCount = 0;
}