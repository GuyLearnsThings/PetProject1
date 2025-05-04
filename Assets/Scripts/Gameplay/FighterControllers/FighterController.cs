using System;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    [SerializeField] private int _startHealthValue;
    [SerializeField] private HealthBar _healthBar;
    
    private Health _health;
    private Transform _currentTarget;
    private bool _isActive;
    private Func<Transform> _getNewTargetCallback;

    public bool IsActive => _isActive;
    public Transform CurrentTarget => _currentTarget;

    private void Awake()
    {
        _health = new Health(_startHealthValue);
        _healthBar.SetHealthForObserve(_health);
    }
    
    public void ActivateFighter(Func<Transform> callback)
    {
        _getNewTargetCallback = callback;
        GetNewTarget();
        _isActive = true;
    }

    public void GetNewTarget()
    {
        _currentTarget = _getNewTargetCallback.Invoke();
    }
}
