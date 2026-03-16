using System;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Weapon[] _weapons;

    private Weapon _activeWeapon;
    private int _currentIndex;

    public event Action<Weapon> OnWeapon;
    public event Action<int> OnSwappedWeapon;
    public event Action OnSwappedSound;

    public int CurrentIndex => _currentIndex;

    public void Init()
    {
        if (_weapons == null || _weapons.Length == 0)
        {
            Debug.LogWarning("PlayerShooter: No weapons assigned.");
            return;
        }

        foreach (Weapon weapon in _weapons)
        {
            weapon.gameObject.SetActive(false);
        }

        SwitchWeapon(_currentIndex);
    }

    public void Attack(Camera camera, Transform playerTransform, LayerMask mask)
    {
        if (_activeWeapon == null)
        {
            return;
        }

        _activeWeapon.Attack(camera, playerTransform, mask);
    }

    public void Reload()
    {
        if (_activeWeapon == null)
        {
            return;
        }

        _activeWeapon.Reload();
    }

    public void Cooldown()
    {
        if (_activeWeapon == null)
        {
            return;
        }

        _activeWeapon.Cooldown();
    }

    public void SelectWeapon(int index)
    {
        if (index < 0 || index >= _weapons.Length)
        {
            return;
        }

        if (_currentIndex == index)
        {
            return;
        }

        SwitchWeapon(index);

        OnSwappedSound?.Invoke();
    }

    public void AddAmmo(AmmoType type, int amount)
    {
        foreach (Weapon weapon in _weapons)
        {
            if (weapon.AmmoType == type)
            {
                weapon.AddAmmo(amount);
                return;
            }
        }
    }

    private void SwitchWeapon(int index)
    {
        if (_activeWeapon != null)
        {
            _activeWeapon.gameObject.SetActive(false);
        }

        _currentIndex = index;
        _activeWeapon = _weapons[_currentIndex];

        _activeWeapon.gameObject.SetActive(true);

        OnWeapon?.Invoke(_activeWeapon);
        OnSwappedWeapon?.Invoke(_currentIndex);
    }
}