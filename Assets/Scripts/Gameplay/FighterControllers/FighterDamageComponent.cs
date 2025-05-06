using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FighterDamageComponent : MonoBehaviour
{
    [SerializeField] private int _damageValue;
    [SerializeField] private float _attackCoolDown;
    [SerializeField] private int _healingOnKillValue;
    [SerializeField] private FighterController _controller;

    private float _attackReset;

    public event UnityAction OnKill;
    

    private void Update()
    {
        _attackReset -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_attackReset <= 0)
        {
            if (collision.collider.TryGetComponent(out FighterController controller))
            {
                DealDamage(controller.Health);
                _attackReset = _attackCoolDown;
               if (controller.Health.CurrentHealth <= 0)
                {
                    OnKill?.Invoke();
                    HealOnLastHit(_controller.Health);
                }
            }
        }
    }

    private void DealDamage(Health health)
    {
        health.TakeDamage(_damageValue);
    }

    private void HealOnLastHit(Health health)
    {
        health.Heal(_healingOnKillValue);
    }
}
