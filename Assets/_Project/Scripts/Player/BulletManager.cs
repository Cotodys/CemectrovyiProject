using UnityEngine;
using UnityEngine.Pool;

public class BulletManager : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _defaultCapacity = 20;
    [SerializeField] private int _maxPoolSize = 50;

    [Header("Weapon Settings")]
    [SerializeField] private float _fireRate = 0.1f;

    [Header("References")]
    [SerializeField] private Transform _firePoint;

    private ObjectPool<Bullet> _pool;
    private Camera _mainCamera;
    private float _nextFireTime;

    private void Awake()
    {
        _mainCamera = Camera.main;

        if (_firePoint == null)
        {
            PlayerMovement playerScript = FindAnyObjectByType<PlayerMovement>();

            if (playerScript != null)
            {
                _firePoint = playerScript.transform;
            }
            else
            {
                Debug.Log("Ошибка. Не найдено местоположение игрока");
            }
        }

        _pool = new ObjectPool<Bullet>(
                createFunc: CreateBullet,
                actionOnGet: OnGetBullet,
                actionOnRelease: OnReleaseBullet,
                actionOnDestroy: OnDestroyBullet,
                collectionCheck: true,
                defaultCapacity: _defaultCapacity,
                maxSize: _maxPoolSize
            );
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + _fireRate;
            Shoot();
        }
    }


    private void Shoot()
    {
        if (_firePoint == null) return; // в ТЗ требуется культура кода и аккуратность. Эта проверка необязательна, но оставляю

        Bullet bullet = _pool.Get();

        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (Vector2)mouseWorldPosition - (Vector2)_firePoint.position;

        bullet.Init(_firePoint.position, shootDirection, ReleaseBullet);
    }

    private Bullet CreateBullet()
    {
        return Instantiate(_bulletPrefab, transform);
    }


    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        if (bullet != null)
        {
            Destroy(bullet.gameObject);
        }
    }

    private void ReleaseBullet(Bullet bullet)
    {
        _pool.Release(bullet);
    }    
}
