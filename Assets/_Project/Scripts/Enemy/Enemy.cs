using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Base Enemy Settings")]
    [SerializeField] protected int _maxHeatlh;
    [SerializeField] protected int _moveSpeed;
    [SerializeField] protected int _goldReward;
    [SerializeField] protected int _damage;

    protected int _currentHeatlh;
    protected Rigidbody2D _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentHeatlh = _maxHeatlh;
    }

    public virtual void TakeDamage(int damage)
    {
        if (_currentHeatlh <= 0)
        {
            return;
        }

        _currentHeatlh -= damage;
        Debug.Log($"[{gameObject.name}] получил урон в размере {damage}. Текущее значение: {_currentHeatlh} единиц здоровья");
        if (_currentHeatlh <= 0)
        {
            Die();
            return;
        }
    }
    protected abstract void Move();

    protected virtual void Die()
    {
        Debug.Log($"[{gameObject.name}] погиб.");
        Destroy(gameObject);
    }
}
