using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet stats")]
    [SerializeField] private float _speedBullet = 12f;

    private Rigidbody2D _rb;
    private Action<Bullet> _returnToPool;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 position, Vector2 direction, Action<Bullet> returnAction)
    {
        transform.position = position;
        _returnToPool = returnAction;

        _rb.linearVelocity = direction.normalized * _speedBullet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Deactive();
    }

    private void OnBecameInvisible()
    {
        Deactive();
    }

    private void Deactive()
    {
        if (_returnToPool != null)
        {
            _returnToPool.Invoke(this);
        }
    }
}

