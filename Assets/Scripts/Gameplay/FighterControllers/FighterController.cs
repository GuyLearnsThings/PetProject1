using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FighterController : MonoBehaviour
{
    [SerializeField] private int _startHealthValue;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private KillCounterObserver _killCounterObserver;


    private Health _health;
    private Transform _currentTarget;
    private bool _isActive;
    private Func<Transform> _getNewTargetCallback;

    public bool IsActive => _isActive;
    public Transform CurrentTarget => _currentTarget;
    public Health Health => _health;

    private void OnEnable()
    {
        _health.FighterDies += OnFighterDeath;
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

    public void OnFighterDeath()
    {
        gameObject.SetActive(false);
    }
    
    public void HealUponReEngage()
    {
        _health.HealToFullUponReEngage();
    }
}
