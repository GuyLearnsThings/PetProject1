using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _count;
    [SerializeField] private Transform _boundary1;
    [SerializeField] private Transform _boundary2;

    private readonly List<GameObject> _pool = new List<GameObject>();

    protected void Initialise(GameObject preFab)
    {
        for (int i = 0; i < _count; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(_boundary1.position.x, _boundary2.position.x), 5, Random.Range(_boundary1.position.z, _boundary2.position.z));
            GameObject spawned = Instantiate(preFab, randomSpawnPosition, Quaternion.identity);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p  => p.activeSelf == false);

        return result != null;
    }

    public GameObject GetRandomActiveCube()
    {
        var listOfActiveObjs = _pool.Where(o => o.activeSelf).ToList();
        return listOfActiveObjs[Random.Range(0, listOfActiveObjs.Count)];
    }
}
