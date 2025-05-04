using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] protected Image _healthBar;

    public void OnValueChanged(int value, int maxValue)
    {
        _healthBar.fillAmount = (float)value / maxValue;
    }
}
