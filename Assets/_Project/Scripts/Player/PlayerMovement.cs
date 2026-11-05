using UnityEngine;

namespace Project.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _smoothTime = 0.05f;

        private Rigidbody2D _rb;
        private Camera _mainCamera;
        private Vector2 _movementDirection;
        private Vector2 _smoothedMovementInput;
        private Vector2 _movementInputVelocity;

        private global::PlayerController _inputActions;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _mainCamera = Camera.main;

            _rb.gravityScale = 0f;
            _rb.angularDamping = 0f;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            _inputActions = new global::PlayerController();
        }

        private void OnEnable()
        {
            _inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Player.Disable();
        }

        private void Update()
        {
            _movementDirection = _inputActions.Player.Move.ReadValue<Vector2>();
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

            float targetAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            _rb.MoveRotation(targetAngle);
        }
    }
}
