using UnityEngine;

public class CubesFactory : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    [SerializeField] private int _minCubesSpawnCount;
    [SerializeField] private int _maxCubesSpawnCount;
    [SerializeField] private Transform _boundary1;
    [SerializeField] private Transform _boundary2;

    [SerializeField] private ObjectPool _objectPool;

    private void Awake()
    {
        _objectPool.Initialise(_prefab);
        SpawnCubes();
        ActivateCubes();
    }

    private void SpawnCubes()
    {
        int count = Random.Range(_minCubesSpawnCount, _maxCubesSpawnCount);

        for (int i = 0; i < count; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(
                Random.Range(_boundary1.position.x, _boundary2.position.x),
                5,
                Random.Range(_boundary1.position.z, _boundary2.position.z));
            
            SpawnCube(randomSpawnPosition);
        }
    }

    private void ActivateCubes()
    {
        var activeCubes = _objectPool.GetActiveObjects();
        
        foreach (var cube in activeCubes)
            ActivateCube(cube);
    }

    private Transform GetRandomTarget()
    {
        return _objectPool.GetRandomActiveCube().transform;
    }

    public void SpawnCubeOnLeftClick(Vector3 positionForSpawn)
    {
        var cube = SpawnCube(positionForSpawn);
        ActivateCube(cube);
    }

    private GameObject SpawnCube(Vector3 spawnPosition)
    {
        var cube = _objectPool.GetObject();
        cube.transform.position = spawnPosition;
        cube.SetActive(true);
        return cube;
    }

    private void ActivateCube(GameObject obj)
    {
        var controller = obj.GetComponent<FighterController>();
        controller.ActivateFighter(GetRandomTarget);
    }
}

