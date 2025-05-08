using NUnit.Framework.Internal.Filters;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private Transform _container;

    private readonly List<GameObject> _pool = new();
    private GameObject _prefabForInstantiate;

    public void Initialise(GameObject prefab)
    {
        _prefabForInstantiate = prefab;
        
        for (int i = 0; i < _count; i++)
        {
            CreateObjectFromPrefab();
        }
    }

    public GameObject GetObject()
    {
        var result = _pool.FirstOrDefault(p  => !p.activeSelf);
        if (result)
            return result;
        
        return CreateObjectFromPrefab();
    }

    private GameObject CreateObjectFromPrefab()
    {
        GameObject spawned = Instantiate(_prefabForInstantiate, _container);
        spawned.SetActive(false);
        _pool.Add(spawned);
        return spawned;
    }

    public GameObject GetRandomActiveCube()
    {
        var listOfActiveObjs = _pool.Where(o => o.activeSelf).ToList();
        return listOfActiveObjs[Random.Range(0, listOfActiveObjs.Count)];
    }

    public List<GameObject> GetActiveObjects()
    {
        return _pool.Where(o => o.activeSelf).ToList();
    }
}
