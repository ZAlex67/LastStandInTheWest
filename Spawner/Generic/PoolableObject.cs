using System;
using UnityEngine;

public abstract class PoolableObject<T> : MonoBehaviour, IPoolable, IInit<T> where T : PoolableObject<T>
{
    [SerializeField] private float _lifetime = 10f;

    private IPool<T> _pool;

    public void Init(IPool<T> pool)
    {
        _pool = pool ?? throw new ArgumentNullException(nameof(pool));
    }

    public virtual void OnSpawn()
    {
        Invoke(nameof(ReturnToPool), _lifetime);
    }

    public virtual void OnDespawn()
    {
        CancelInvoke();
    }

    protected void ReturnToPool()
    {
        if (_pool == null)
        {
            Debug.LogError($"{name} pool not initialized");
            return;
        }

        _pool.Despawn((T)this);
    }
}