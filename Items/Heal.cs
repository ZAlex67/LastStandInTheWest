using System;
using UnityEngine;

public class Heal : PoolableObject<Heal>, IPickup
{
    [SerializeField] private float _healAmount = 20f;

    public float HealAmount => _healAmount;

    public static event Action OnHealPickedUp;

    public void Collect(PlayerCollector collector)
    {
        if (collector.TryGetComponent<IHeal>(out var healer))
        {
            healer.RestoreHealth(_healAmount);
        }

        OnHealPickedUp?.Invoke();
        ReturnToPool();
    }
}