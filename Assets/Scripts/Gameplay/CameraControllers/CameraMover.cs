using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
    // TODO: Переименовать как сенса
    // TODO: Проверить, насколько я помню её нужно выключать
    [SerializeField] private InputReader _inputReader;

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 40f;
    [SerializeField] private float _verticalSpeed = 10f;

    [Header("Mouse Settings")]
    [SerializeField] private float _lookSensitivity = 0.5f;

    //private bool _isLooking;
    private float _xRotation = 0f;
    private float _yRotation = 0f;


    private Camera _camera;
    private InputSystem _inputSystem;


    private void Awake()
    {
        _camera = Camera.main;

    }

    private void Update()
    {
        LookAround();
        MoveCamera();
    }

    private void MoveCamera()
    {
        float speed = _moveSpeed;
        Vector3 forwardMovement = Vector3.Normalize(transform.forward) * _inputReader.MoveInput.y * speed * Time.deltaTime;
        Vector3 rightMovement = Vector3.Normalize(transform.right) * _inputReader.MoveInput.x * speed * Time.deltaTime;

        transform.position += forwardMovement + rightMovement + (_inputReader.VerticalMovement * _verticalSpeed * Time.deltaTime);
    }

    private void LookAround()
    {
        float mouseX = _inputReader.LookInput.x * _lookSensitivity * Time.deltaTime;
        float mouseY = _inputReader.LookInput.y * _lookSensitivity * Time.deltaTime;

        _yRotation += mouseX;
        _xRotation = Mathf.Clamp(_xRotation - mouseY, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
    }
}
