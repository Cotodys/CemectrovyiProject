using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float _smoothTime = 0.05f;

    private Rigidbody2D _rb;
    private Camera _mainCamera;
    private Vector2 _movementDirection;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;

        _rb.gravityScale = 0f;
        _rb.angularDamping = 0f;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        _movementDirection.x = Input.GetAxisRaw("Horizontal");
        _movementDirection.y = Input.GetAxisRaw("Vertical");

        if (_movementDirection.sqrMagnitude > 1)
        {
            _movementDirection.Normalize();
        }
    }

    private void FixedUpdate()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _movementDirection,
            ref _movementInputVelocity,
            _smoothTime
            );

        _rb.linearVelocity = _smoothedMovementInput * _moveSpeed;

        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        Vector2 lookDirection = (Vector2)mouseWorldPosition - _rb.position;

        float targetAngle = Mathf.Atan2(lookDirection.y , lookDirection.x) * Mathf.Rad2Deg;

        _rb.MoveRotation(targetAngle);
    }
}
