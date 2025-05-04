using UnityEngine;

public class CameraMover : MonoBehaviour
{
    // TODO: Переименовать как сенса
    // TODO: Проверить, насколько я помню её нужно выключать
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed; // TODO: Переименовать как сенса

    private InputSystem _input;
    private Vector2 _direction;
    
    private Vector2 _inputRotation;
    private Vector2 _currentCameraRotation;

    private void Awake()
    {
        _input = new InputSystem();
        _input.Enable(); // TODO: Проверить, насколько я помню её нужно выключать
    }
    
    private void Update()
    {
        _inputRotation = _input.Player.Look.ReadValue<Vector2>();
        _direction = _input.Player.Move.ReadValue<Vector2>();

        Look(_inputRotation);
        Move(_direction);
    }   

    private void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.1)
            return;

        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
        transform.position += move * scaledMoveSpeed;
    }

    private void Look(Vector2 rotate)
    {
        if (rotate.sqrMagnitude < 0.01)
            return;

        float scaledRotateSpeed = _rotateSpeed * Time.deltaTime;
        _currentCameraRotation.y += rotate.x * scaledRotateSpeed;
        _currentCameraRotation.x = Mathf.Clamp(_currentCameraRotation.x - rotate.y * scaledRotateSpeed, -90, 90);
        transform.localEulerAngles = _currentCameraRotation;
    }
}
