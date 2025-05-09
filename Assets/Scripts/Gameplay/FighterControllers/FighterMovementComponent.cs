using UnityEngine;
using UnityEngine.AI;

public class FighterMovementComponent : MonoBehaviour
{
    [SerializeField] private FighterController _fighterController;
    [SerializeField] private float _speed;
    
    private NavMeshAgent _navMeshAgent;
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        if (!_fighterController.IsActive)
            return;
        
        if (_fighterController.CurrentTarget != null && _fighterController.CurrentTarget.gameObject.activeSelf)
        {
            _navMeshAgent.destination =_fighterController.CurrentTarget.position;
        }
        else
            GetNewTarget();
    }

    private void GetNewTarget()
    {
        _fighterController.GetNewTarget();
    }
}
