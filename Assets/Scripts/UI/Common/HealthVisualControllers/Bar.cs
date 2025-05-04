using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Image _fillingImage;

    protected void OnValueChanged(int currentValue, int maxValue)
    {
        _fillingImage.fillAmount = (float)currentValue / maxValue;
    }
}
