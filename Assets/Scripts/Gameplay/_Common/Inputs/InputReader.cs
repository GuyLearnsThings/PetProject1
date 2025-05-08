using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private InputSystem _inputSystem;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private Vector3 _verticalMovement;
    private bool _leftClickPerformed;
    public Vector2 MoveInput => _moveInput;
    public Vector2 LookInput => _lookInput;
    public Vector3 VerticalMovement => _verticalMovement;

    public event UnityAction LMBPressed; // TODO: Конкретная клавиша в названии, клавиши - настраиваемый элемент игроком.
    private void OnEnable()
    {
        // ������ ��� � ��������� ������, ������� ��� �������� � ������ ��������, ���������� ������ ����� �� �� ����
        _inputSystem = new InputSystem();
        _inputSystem.Enable();

        _inputSystem.GameActionMap.Move.performed += OnMove;
        _inputSystem.GameActionMap.Move.canceled += OnMove;

        _inputSystem.GameActionMap.MoveUp.performed += OnMoveUp;
        _inputSystem.GameActionMap.MoveUp.canceled += OnMoveUp;

        _inputSystem.GameActionMap.MoveDown.performed += OnMoveDown;
        _inputSystem.GameActionMap.MoveDown.canceled += OnMoveDown;

        _inputSystem.GameActionMap.Look.performed += OnLook;
        _inputSystem.GameActionMap.Look.canceled += OnLook;

        _inputSystem.GameActionMap.LeftClick.performed += OnLeftClick;
    }

    private void OnDisable()
    {
        _inputSystem.GameActionMap.Move.performed -= OnMove;
        _inputSystem.GameActionMap.Move.canceled -= OnMove;

        _inputSystem.GameActionMap.MoveUp.performed -= OnMoveUp;
        _inputSystem.GameActionMap.MoveUp.canceled -= OnMoveUp;

        _inputSystem.GameActionMap.MoveDown.performed -= OnMoveDown;
        _inputSystem.GameActionMap.MoveDown.canceled -= OnMoveDown;

        _inputSystem.GameActionMap.Look.performed -= OnLook;
        _inputSystem.GameActionMap.Look.canceled -= OnLook;

        _inputSystem.GameActionMap.LeftClick.performed -= OnLeftClick;

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
    private void OnLeftClick(InputAction.CallbackContext context)
    {
        LMBPressed?.Invoke();
    }
}
