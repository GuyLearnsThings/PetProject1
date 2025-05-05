using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
    // TODO: Переименовать как сенса
    // TODO: Проверить, насколько я помню её нужно выключать

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 40f;
    [SerializeField] private float _verticalSpeed = 10f;

    [Header("Mouse Settings")]
    [SerializeField] private float _lookSensitivity = 0.5f;

    //private bool _isLooking;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private Vector3 _verticalMovement = Vector3.zero;
    private float _xRotation = 0f;
    private float _yRotation = 0f;


    private Camera _camera;
    private InputSystem _inputSystem;


    private void Awake()
    {
        _camera = Camera.main;

    }

    private void OnEnable()
    {
        _inputSystem = new InputSystem();
        _inputSystem.Enable();

        _inputSystem.CameraMover.Move.performed += OnMove;
        _inputSystem.CameraMover.Move.canceled += OnMove;

        _inputSystem.CameraMover.MoveUp.performed += OnMoveUp;
        _inputSystem.CameraMover.MoveUp.canceled += OnMoveUp;

        _inputSystem.CameraMover.MoveDown.performed += OnMoveDown;
        _inputSystem.CameraMover.MoveDown.canceled += OnMoveDown;

        _inputSystem.CameraMover.Look.performed += OnLook;
        _inputSystem.CameraMover.Look.canceled += OnLook;

        //_inputSystem.CameraMover.RightClick.performed += OnRightClick;
        //_inputSystem.CameraMover.RightClick.canceled += OnRightClick;

    }

    private void OnDisable()
    {
        _inputSystem.CameraMover.Move.performed -= OnMove;
        _inputSystem.CameraMover.Move.canceled -= OnMove;

        _inputSystem.CameraMover.MoveUp.performed -= OnMoveUp;
        _inputSystem.CameraMover.MoveUp.canceled -= OnMoveUp;

        _inputSystem.CameraMover.MoveDown.performed -= OnMoveDown;
        _inputSystem.CameraMover.MoveDown.canceled -= OnMoveDown;

        _inputSystem.CameraMover.Look.performed -= OnLook;
        _inputSystem.CameraMover.Look.canceled -= OnLook;

        //_inputSystem.CameraMover.RightClick.performed -= OnRightClick;
        //_inputSystem.CameraMover.RightClick.canceled -= OnRightClick;

        _inputSystem.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

    }

    private void OnMoveUp(InputAction.CallbackContext context)
    {
        _verticalMovement = context.performed ? Vector3.up : Vector3.zero;
    }

    private void OnMoveDown(InputAction.CallbackContext context)
    {
        _verticalMovement = context.performed ? Vector3.down : Vector3.zero;
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }

    //private void OnRightClick(InputAction.CallbackContext context)
    //{
    //    _isLooking = context.performed;
    //}

    private void Update()
    {
        LookAround();
        MoveCamera();
    }

    private void MoveCamera()
    {
        float speed = _moveSpeed;
        Vector3 forwardMovement = Vector3.Normalize(transform.forward) * _moveInput.y * speed * Time.deltaTime;
        Vector3 rightMovement = Vector3.Normalize(transform.right) * _moveInput.x * speed * Time.deltaTime;

        transform.position += forwardMovement + rightMovement + (_verticalMovement * _verticalSpeed * Time.deltaTime);
    }

    private void LookAround()
    {
        float mouseX = _lookInput.x * _lookSensitivity * Time.deltaTime;
        float mouseY = _lookInput.y * _lookSensitivity * Time.deltaTime;

        _yRotation += mouseX;
        _xRotation = Mathf.Clamp(_xRotation - mouseY, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
    }
}
