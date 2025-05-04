using System.Diagnostics;
using UnityEngine.Events;

public class Health
{
    private readonly int _maxHealth;
    private int _currentHealth;

    public event UnityAction<int, int> OnHealthChanged;
    public event UnityAction FighterDies;

    public Health(int healthValue)
    {
        _maxHealth = healthValue;
        _currentHealth = healthValue;
    }
    
    
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage; // TODO: Закончить нанесение урона + сделдать метод для хила
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        if (_currentHealth <= 0)
        {
            FighterDies?.Invoke();
        }
    }
}
