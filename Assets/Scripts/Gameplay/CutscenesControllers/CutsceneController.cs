using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CameraMover _camera;
    [SerializeField] private ParticleSystem _finalCutsceneFX;

    public void StartFinalCutscene(Transform targetLook, Transform targetMove)
    {
        _inputReader.DisableForCutscene();
        _camera.MoveToWinner(targetMove, targetLook);
        _finalCutsceneFX.transform.position = targetLook.position;
        _finalCutsceneFX.Play();
        Debug.Log("Final");
        Debug.Log(targetMove);
    }
}