using System;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    [SerializeField] private int _startHealthValue;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Transform _waypointForFinal;

    private Health _health;
    private Transform _currentTarget;
    private Func<Transform> _getNewTargetCallback;
    private Action _onKillCallback;
    private bool _isActive;

    public bool IsActive => _isActive;
    public Transform CurrentTarget => _currentTarget;
    public Health Health => _health;
    public Transform WayPointForFinal => _waypointForFinal; 

    private void OnEnable()
    {
        _health.FighterDies += OnFighterDeath;
        _health.Heal(999);
    }

    private void OnDisable()
    {
        _health.FighterDies -= OnFighterDeath;
    }
    
    private void Awake()
    {
        _health = new Health(_startHealthValue);
        _healthBar.SetHealthForObserve(_health);
    }
    
    public void ActivateFighter(Func<Transform> getNewTargetCallback, Action onKillCallback)
    {
        _getNewTargetCallback = getNewTargetCallback;
        _onKillCallback = onKillCallback;
        GetNewTarget();
        _isActive = true;
    }

    public void GetNewTarget()
    {
        _currentTarget = _getNewTargetCallback?.Invoke();
    }

    private void OnFighterDeath()
    {
        gameObject.SetActive(false);
        _onKillCallback?.Invoke();
    }
}
