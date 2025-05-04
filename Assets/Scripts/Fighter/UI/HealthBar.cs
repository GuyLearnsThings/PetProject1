using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBar : Bar
{
    [SerializeField] private Fighter _fighter;

    private void OnEnable()
    {
        _healthBar.fillAmount = 1;
        _fighter.OnHealthChange += OnValueChanged;
    }

    private void OnDisable()
    {
        _fighter.OnHealthChange -= OnValueChanged;
    }
}
