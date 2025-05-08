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
    
    public bool TakeDamage(int damage)
    {
        _currentHealth -= damage;
        
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            FighterDies?.Invoke();
            return true;
        }
        
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        return false;
    }

    public void Heal(int healValue)
    {
        _currentHealth += healValue;
        
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
        
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
}
