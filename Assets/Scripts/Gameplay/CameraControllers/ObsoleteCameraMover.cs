using UnityEngine;

public class ObsoleteCameraMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _sensitivity;

    private InputSystem _input;
    private Vector2 _direction;
    private Vector2 _rotate;
    private Vector2 _rotation;


    private void Awake()
    {
        _input = new InputSystem();
        _input.Enable();

    }

    private void Update()
    {
        _rotate = _input.CameraMover.Look.ReadValue<Vector2>();
        _direction = _input.CameraMover.Move.ReadValue<Vector2>();

        Look(_rotate);
        Move(_direction);
    }

    private void OnDisable()
    {
        _input.Disable();
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

        float scaledRotateSpeed = _sensitivity * Time.deltaTime;
        _rotation.y += rotate.x * scaledRotateSpeed;
        _rotation.x = Mathf.Clamp(_rotation.x - rotate.y * scaledRotateSpeed, -90, 90);
        transform.localEulerAngles = _rotation;
    }
}
