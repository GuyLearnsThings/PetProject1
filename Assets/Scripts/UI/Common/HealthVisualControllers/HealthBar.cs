
public class HealthBar : Bar
{
    private Health _healthForObserve;
    
    public void SetHealthForObserve(Health health)
    {
        _healthForObserve = health;
    }

    private void OnEnable()
    {
        _healthForObserve.OnHealthChanged += OnValueChanged;
        _fillingImage.fillAmount = 1;
    }

    private void OnDisable()
    {
        if (_healthForObserve != null)
            _healthForObserve.OnHealthChanged -= OnValueChanged;
    }
}
