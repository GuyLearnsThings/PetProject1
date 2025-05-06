using UnityEngine;
using UnityEngine.Events;


public class SpawnCubeOnLMB : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    public event UnityAction CreateCube;

    private void OnEnable()
    {
        _inputReader.LMBPressed += CreateCubeOnLMB;
    }
    private void OnDisable()
    {
        _inputReader.LMBPressed -= CreateCubeOnLMB;
    }

    public void CreateCubeOnLMB()
    {
        CreateCube?.Invoke();
    }
}
