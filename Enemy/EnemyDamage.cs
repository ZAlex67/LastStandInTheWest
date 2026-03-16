using System;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float _damageAmount = 10f;
    [SerializeField] private float _attackCooldown = 1f;

    private float _nextAttackTime;

    public static event Action OnPlayerDamageSound;

    private void OnTriggerStay(Collider collision)
    {
        if (Time.time < _nextAttackTime)
        {
            return;
        }

        if (collision.TryGetComponent<IDamageable>(out IDamageable target))
        {
            OnPlayerDamageSound?.Invoke();
            target.TakeDamage(_damageAmount);
            _nextAttackTime = Time.time + _attackCooldown;
        }
    }
}