using System;
using UnityEngine;

public class Ammo : PoolableObject<Ammo>, IPickup
{
    [SerializeField] private AmmoType _ammoType;

    [SerializeField] private GameObject _revolverModel;
    [SerializeField] private GameObject _rifleModel;

    private int _amount;

    public AmmoType AmmoType => _ammoType;
    public int Amount => _amount;

    public static event Action OnAmmoPickedUp;

    public override void OnSpawn()
    {
        base.OnSpawn();
        UpdateVisual();
    }

    public void Collect(PlayerCollector collector)
    {
        if (collector.TryGetComponent<PlayerShooter>(out var shooter))
        {
            shooter.AddAmmo(_ammoType, _amount);
        }

        OnAmmoPickedUp?.Invoke();
        ReturnToPool();
    }

    public void Configure(AmmoType type, int amount)
    {
        _ammoType = type;
        _amount = amount;

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (_revolverModel != null)
        {
            _revolverModel.SetActive(_ammoType == AmmoType.Revolver);
        }

        if (_rifleModel != null)
        {
            _rifleModel.SetActive(_ammoType == AmmoType.Rifle);
        }
    }
}