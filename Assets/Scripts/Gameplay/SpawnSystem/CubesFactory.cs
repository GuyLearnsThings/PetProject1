using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubesFactory : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    [SerializeField] private int _minCubesSpawnCount;
    [SerializeField] private int _maxCubesSpawnCount;
    [SerializeField] private Transform _boundary1;
    [SerializeField] private Transform _boundary2;

    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private SpawnCubeOnLMB _cubeCreator;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _objectPool.Initialise(_prefab);
        SpawnCubes();
        ActivateCubes();
    }

    private void OnEnable()
    {
        _cubeCreator.CreateCube += SpawnCubeOnLeftClick;
    }

    private void OnDisable()
    {
        _cubeCreator.CreateCube -= SpawnCubeOnLeftClick;
    }

    private void SpawnCubes()
    {
        int count = Random.Range(_minCubesSpawnCount, _maxCubesSpawnCount);

        for (int i = 0; i < count; i++)
        {
            var cube = _objectPool.GetObject();
            Vector3 randomSpawnPosition = new Vector3(
                Random.Range(_boundary1.position.x, _boundary2.position.x),
                5,
                Random.Range(_boundary1.position.z, _boundary2.position.z));

            cube.transform.position = randomSpawnPosition;
            cube.SetActive(true);
        }
    }

    private void ActivateCubes()
    {
        var activeCubes = _objectPool.GetActiveObjects();
        foreach (var cube in activeCubes)
        {
            var controller = cube.GetComponent<FighterController>();
            controller.ActivateFighter(GetRandomTarget);
        }
    }

    private Transform GetRandomTarget()
    {
        return _objectPool.GetRandomActiveCube().transform;
    }

    private void SpawnCubeOnLeftClick()
    {
        var cube = _objectPool.GetObject();
        var controller = cube.GetComponent<FighterController>();
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            cube.transform.position = hit.point;
            controller.ActivateFighter(GetRandomTarget);
            cube.SetActive(true);
            controller.HealUponReEngage();
        }
    }
}

