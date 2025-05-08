using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FighterDamageComponent : MonoBehaviour
{
    [SerializeField] private int _damageValue;
    [SerializeField] private int _healingOnKillValue;
    [SerializeField] private float _attackCoolDown;
    [SerializeField] private FighterController _controller;
    
    private bool _isReload;
    public event UnityAction OnKill;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (_isReload)
            return;
        
        if (collision.collider.TryGetComponent(out FighterController controller))
            DealDamage(controller.Health);
    }
    
    private void DealDamage(Health health)
    {
        var isKilled = health.TakeDamage(_damageValue);
        
        if (gameObject.activeSelf)
            StartCoroutine(ReloadCoroutine());

        if (isKilled)
            EffectOnKill();
    }
    
    private IEnumerator ReloadCoroutine()
    {
        _isReload = true;
        yield return new WaitForSeconds(_attackCoolDown);
        _isReload = false;
    }

    private void EffectOnKill()
    {
        OnKill?.Invoke();
        _controller.Health.Heal(_healingOnKillValue);
    }
}
