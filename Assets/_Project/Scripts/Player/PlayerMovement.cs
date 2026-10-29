using UnityEngine;
using UnityEngine.Pool;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedPlayer = 5f;
    private Vector2 _movePlayer;
    private ObjectPool<GameObject> _objPool;
    private Vector3 _mousePixels = Input.mousePosition;

    void Update()
    {
        _movePlayer = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _movePlayer += new Vector2(0f, 1f) * _speedPlayer * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _movePlayer += new Vector2(0f, -1f) * _speedPlayer * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _movePlayer += new Vector2(1f, 0f) * _speedPlayer * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _movePlayer += new Vector2(-1f, 0f) * _speedPlayer * Time.deltaTime;
        }

        transform.position = (Vector2)transform.position + _movePlayer;


        Vector3 mousePixels = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePixels);
        Vector2 lookDirection = (Vector2)mouseWorldPosition - (Vector2)transform.position;

        float targetAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        float smoothAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, 0.1f);
        transform.rotation = Quaternion.Euler(0, 0, smoothAngle);

    }
}
