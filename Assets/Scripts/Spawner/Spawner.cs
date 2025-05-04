using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Spawner : ObjectPool
{
    [SerializeField] private GameObject _template;
    [SerializeField] private int _minCubesSpawnCount;
    [SerializeField] private int _maxCubesSpawnCount;

    private void Start()
    {
        Initialise(_template);
        SpawnCubes();
    }

    private void SpawnCubes()
    {     
        int count = Random.Range(_minCubesSpawnCount, _maxCubesSpawnCount);

        for (int i = 0; i < count; i++)
        {
            if (TryGetObject(out GameObject cubeGameObj))
            {
                Fighter fighter = cubeGameObj.GetComponent<Fighter>();
                fighter.SetInitData(this);

                cubeGameObj.SetActive(true);
            }
        }
    }

    public Transform GetNewTargetTransform()
    {
        return GetRandomActiveCube().transform;
    }
}
