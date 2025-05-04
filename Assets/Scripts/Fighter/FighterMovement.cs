using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class FighterMovement : MonoBehaviour
{
    [SerializeField] private Fighter _fighter;
    private NavMeshAgent _navMeshAgent;
    private Transform _currentTarget;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_currentTarget != null && _currentTarget.gameObject.activeSelf)
        {
            _navMeshAgent.destination =_currentTarget.position;
        }
        else
            GetNewTarget();
    }

    private void GetNewTarget()
    {
        _currentTarget = _fighter.GetNewTarget();
    }
}
