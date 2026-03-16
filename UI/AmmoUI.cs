using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _maxAmmoText;
    [SerializeField] private TextMeshProUGUI _currentAmmoText;
    [SerializeField] private Slider _cooldownSlider;
    [SerializeField] private PlayerShooter _shooter;

    private Weapon _currentWeapon;

    private void OnEnable()
    {
        _shooter.OnWeapon += UpdateCurrentWeapon;
    }

    private void OnDisable()
    {
        _shooter.OnWeapon -= UpdateCurrentWeapon;
        UnsubscribeFromWeapon();
    }

    private void UpdateMaxAmmo(int maxAmmo)
    {
        _maxAmmoText.text = maxAmmo.ToString();
    }

    private void UpdateCurrentAmmo(int currentAmmo)
    {
        _currentAmmoText.text = currentAmmo.ToString();
    }

    private void UpdateCooldown(float cooldown)
    {
        _cooldownSlider.value = cooldown;
    }

    private void InitializeCooldownSlider()
    {
        _cooldownSlider.maxValue = _currentWeapon.CooldownTime;
        _cooldownSlider.value = _currentWeapon.CooldownTime;
    }

    private void UpdateCurrentWeapon(Weapon weapon)
    {
        UnsubscribeFromWeapon();

        _currentWeapon = weapon;

        if (_currentWeapon == null)
        {
            return;
        }

        _currentWeapon.OnMaxAmmoChanged += UpdateMaxAmmo;
        _currentWeapon.OnCurrentAmmoChanged += UpdateCurrentAmmo;
        _currentWeapon.OnCooldownChanged += UpdateCooldown;

        InitializeCooldownSlider();
        UpdateMaxAmmo(_currentWeapon.MaxAmmo);
        UpdateCurrentAmmo(_currentWeapon.CurrentAmmo);
    }

    private void UnsubscribeFromWeapon()
    {
        if (_currentWeapon == null)
        {
            return;
        }

        _currentWeapon.OnMaxAmmoChanged -= UpdateMaxAmmo;
        _currentWeapon.OnCurrentAmmoChanged -= UpdateCurrentAmmo;
        _currentWeapon.OnCooldownChanged -= UpdateCooldown;
    }
}