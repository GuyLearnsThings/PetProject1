
public class HealthBar : Bar
{
    private Health _healthForObserve;
    
    public void SetHealthForObserve(Health health)
    {
        _healthForObserve = health;
        _healthForObserve.OnHealthChanged += OnValueChanged;
    }
    
    private void OnDisable()
    {
        if (_healthForObserve != null)
            _healthForObserve.OnHealthChanged -= OnValueChanged;
    }
    
    private void Start()
    {
        _fillingImage.fillAmount = 1;
    }
}
