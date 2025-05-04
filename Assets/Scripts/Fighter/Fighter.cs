using System;
using UnityEngine;
using UnityEngine.Events;

public class Fighter : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    private int _currentHealth;

    public event UnityAction<int, int> OnHealthChange;
    private Spawner _spawner;

    private void Start()
    {
        _currentHealth = _health;
    }

    public void SetInitData(Spawner spawner)
    {
        _spawner = spawner;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnHealthChange?.Invoke(_currentHealth, _health);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(_damage);
        }
    }

    public Transform GetNewTarget()
    {
        return _spawner.GetNewTargetTransform();
    }
}
