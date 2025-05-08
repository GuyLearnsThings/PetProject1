using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnCubeOnScreenTap : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CubesFactory _cubesFactory;

    private Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _inputReader.LMBPressed += CreateCubeOnLMB;
    }
    private void OnDisable()
    {
        _inputReader.LMBPressed -= CreateCubeOnLMB;
    }

    private void CreateCubeOnLMB()
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
            _cubesFactory.SpawnCubeOnLeftClick(hit.point);
    }
}
