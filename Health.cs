using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IPoolable, IHeal
{
    [SerializeField] private float _maxHealth = 100f;

    private float _currentHealth;
    private bool _isDead;

    public event Action OnDamageTaken;
    public event Action OnHealthChanged;
    public event Action OnDeath;


    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    private void Awake()
    {
        ResetHealth();
    }

    public void OnSpawn()
    {
        ResetHealth();
    }

    public void OnDespawn()
    {
        StopAllCoroutines();
    }

    public void TakeDamage(float damage)
    {
        if (_isDead)
        {
            return;
        }

        if (damage <= 0)
        {
            return;
        }

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);

        OnDamageTaken?.Invoke();
        OnHealthChanged?.Invoke();

        if (_currentHealth <= 0 && !_isDead)
        {
            _isDead = true;
            OnDeath?.Invoke();
        }
    }

    public void RestoreHealth(float health)
    {
        if (health <= 0)
        {
            return;
        }

        _currentHealth = Mathf.Clamp(_currentHealth + health, 0, _maxHealth);

        OnHealthChanged?.Invoke();
    }

    private void ResetHealth()
    {
        _currentHealth = _maxHealth;
        _isDead = false;
        OnHealthChanged?.Invoke();
    }
}