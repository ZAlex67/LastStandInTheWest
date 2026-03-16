using System;
using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public abstract class SpawnerGeneric<T> : MonoBehaviour, IPool<T> where T : MonoBehaviour, IPoolable
{
    [SerializeField] private GenericPoolConfig<T> _poolConfig;

    private ObjectPool<T> _pool;

    public event Action<int> TotalCountChanged;
    public event Action<int> ActiveCountChanged;

    protected virtual void Awake()
    {
        if (!ValidateConfig())
        {
            return;
        }

        _pool = new ObjectPool<T>(
            CreateInstance,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPoolObject,
            collectionCheck: false,
            _poolConfig.DefaultCapacity,
            _poolConfig.MaxSize
        );

        if (_poolConfig.WarmUp)
        {
            WarmUp();
        }
    }

    public T Spawn(in Vector3 position, in Quaternion rotation)
    {
        T obj = _pool.Get();

        SetPosition(obj, position, rotation);

        return obj;
    }

    public void Despawn(T obj)
    {
        _pool.Release(obj);
    }

    protected virtual void SetPosition(T obj, Vector3 position, Quaternion rotation)
    {
        obj.transform.SetPositionAndRotation(position, rotation);
    }

    protected virtual T CreateInstance()
    {
        T instance = Instantiate(_poolConfig.Prefab, transform);
        instance.gameObject.SetActive(false);

        InitInstance(instance);

        TotalCountChanged?.Invoke(_pool.CountAll);

        return instance;
    }

    protected virtual void OnGetFromPool(T obj)
    {
        obj.gameObject.SetActive(true);
        obj.OnSpawn();
        ActiveCountChanged?.Invoke(_pool.CountActive);
    }

    protected virtual void OnReleaseToPool(T obj)
    {
        obj.OnDespawn();
        obj.gameObject.SetActive(false);
        ActiveCountChanged?.Invoke(_pool.CountActive);
    }

    protected virtual void OnDestroyPoolObject(T obj)
    {
        Destroy(obj.gameObject);
    }

    protected virtual void InitInstance(T instance)
    {
        if (instance is IInit<T> initializer)
        {
            initializer.Init(this);
        }
    }

    private void WarmUp()
    {
        var instances = new List<T>(_poolConfig.DefaultCapacity);

        for (int i = 0; i < _poolConfig.DefaultCapacity; i++)
        {
            instances.Add(_pool.Get());
        }

        foreach (var obj in instances)
        {
            _pool.Release(obj);
        }
    }

    private bool ValidateConfig()
    {
        if (_poolConfig == null)
        {
            return LogError("PoolConfig is missing");
        }

        if (_poolConfig.Prefab == null)
        {
            return LogError("PoolConfig Prefab is not assigned");
        }

        return true;
    }

    private bool LogError(string message)
    {
        Debug.LogError($"{name}: {message}");
        enabled = false;
        return false;
    }
}