using System;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData _data;
    [SerializeField] private Transform _firePoint;

    protected int _maxAmmo;
    protected int _currentAmmo;
    protected float _cooldown;

    public int MaxAmmo => _maxAmmo;
    public int CurrentAmmo => _currentAmmo;
    public float CooldownTime => _cooldown;
    public AmmoType AmmoType => _data.AmmoType;

    protected WeaponData Data => _data;
    protected Transform FirePoint => _firePoint;

    public event Action<int> OnMaxAmmoChanged;
    public event Action<int> OnCurrentAmmoChanged;
    public event Action<float> OnCooldownChanged;

    public event Action<Vector3, Quaternion> OnSpawnHitEffect;
    public event Action<Vector3, Quaternion> OnSpawnShootEffect;
    public event Action<Vector3, Quaternion> OnSpawnCollisionHitEffect;

    public event Action<AudioClip> OnShootSound;
    public event Action OnReloadSound;
    public event Action OnStopSound;
    public event Action OnHitEnemySound;

    protected virtual void Awake()
    {
        if (_data == null)
        {
            Debug.LogError($"{nameof(WeaponData)} is not assigned", this);
            enabled = false;
            return;
        }

        _maxAmmo = _data.MaxAmmo;
        _cooldown = _data.Cooldown;
        _currentAmmo = _data.CurrentAmmo;
    }

    protected virtual void Start()
    {
        OnMaxAmmoChanged?.Invoke(_maxAmmo);
        OnCurrentAmmoChanged?.Invoke(_currentAmmo);
        OnCooldownChanged?.Invoke(_cooldown);
    }

    public abstract void Attack(Camera camera, Transform playerTransform, LayerMask mask);

    public virtual void Reload() { }

    public virtual void Cooldown() { }

    public virtual void AddAmmo(int amount)
    {
        _maxAmmo += amount;
        InvokeMaxAmmoChanged();
    }

    protected virtual void InvokeMaxAmmoChanged()
    {
        OnMaxAmmoChanged?.Invoke(_maxAmmo);
    }

    protected virtual void InvokeCurrentAmmoChanged()
    {
        OnCurrentAmmoChanged?.Invoke(_currentAmmo);
    }

    protected virtual void InvokeCooldownChanged()
    {
        OnCooldownChanged?.Invoke(_cooldown);
    }

    protected virtual void InvokeSpawnHitEffect(Vector3 position, Quaternion rotation)
    {
        OnSpawnHitEffect?.Invoke(position, rotation);
    }

    protected virtual void InvokeSpawnShootEffect(Vector3 position, Quaternion rotation)
    {
        OnSpawnShootEffect?.Invoke(position, rotation);
    }

    protected virtual void InvokeSpawnCollisionHitEffect(Vector3 position, Quaternion rotation)
    {
        OnSpawnCollisionHitEffect?.Invoke(position, rotation);
    }

    protected virtual void InvokeShootSound()
    {
        OnShootSound?.Invoke(Data.ShootSound);
    }

    protected virtual void InvokeReloadSound()
    {
        OnReloadSound?.Invoke();
    }

    protected virtual void InvokeStopSound()
    {
        OnStopSound?.Invoke();
    }

    protected virtual void InvokeHitEnemySound()
    {
        OnHitEnemySound?.Invoke();
    }
}