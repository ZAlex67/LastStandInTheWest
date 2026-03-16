using UnityEngine;

public abstract class GenericPoolConfig<T> : ScriptableObject where T : Component
{
    [Header("Prefab")]
    [SerializeField] private T _prefab;
    public T Prefab => _prefab;

    [Header("Pool Size"), Range(1, 1000)]
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 50;

    public int DefaultCapacity => _defaultCapacity;
    public int MaxSize => _maxSize;

    [Header("Behavior")]
    [SerializeField] private bool _collectionCheck = false;
    [SerializeField] private bool _warmUp = true;

    public bool CollectionCheck => _collectionCheck;
    public bool WarmUp => _warmUp;

    private void OnValidate()
    {
        if (_defaultCapacity > _maxSize)
        {
            _defaultCapacity = _maxSize;

            Debug.LogWarning($"{name}: DefaultCapacity clamped to MaxSize");
        }

        if (_prefab == null)
        {
            Debug.LogWarning($"{name}: Prefab is not assigned");
        }
    }
}