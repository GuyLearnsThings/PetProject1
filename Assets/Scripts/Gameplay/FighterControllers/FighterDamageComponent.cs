using Unity.VisualScripting;
using UnityEngine;

public class FighterDamageComponent : MonoBehaviour
{
    [SerializeField] private int _damageValue;
    [SerializeField] private float _attackCoolDown;
    private float _attackReset;

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
            }
        }
    }

    private void DealDamage(Health health)
    {
        health.TakeDamage(_damageValue);
    }
}
