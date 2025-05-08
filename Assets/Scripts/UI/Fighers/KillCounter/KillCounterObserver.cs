using TMPro;
using UnityEngine;

public class KillCounterObserver : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterLabel;
    [SerializeField] private FighterDamageComponent _controllerForObserve;

    private int _currentValue;

    private void OnEnable()
    {
        _controllerForObserve.OnKill += CounterChanged;
    }

    private void OnDisable()
    {
        _controllerForObserve.OnKill -= CounterChanged;
    }

    private void Awake()
    {
        _counterLabel.text = _currentValue.ToString();
    }
    private void CounterChanged()
    {
        _currentValue++;
        _counterLabel.text = _currentValue.ToString();
    }
}
