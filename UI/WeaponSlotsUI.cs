using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotsUI : MonoBehaviour
{
    [SerializeField] private Image[] _slotSelectionImages;
    [SerializeField] private PlayerShooter _playerShooter;

    private int _currentIndex = -1;

    private void Awake()
    {
        Debug.Assert(_playerShooter != null);
    }

    private void OnEnable()
    {
        _playerShooter.OnSwappedWeapon += UpdateWeaponSlot;
    }

    private void OnDisable()
    {
        _playerShooter.OnSwappedWeapon -= UpdateWeaponSlot;
    }

    private void UpdateWeaponSlot(int index)
    {
        if (index == _currentIndex)
        {
            return;
        }

        if (index < 0 || index >= _slotSelectionImages.Length)
        {
            return;
        }

        DisableCurrentSlot();

        _slotSelectionImages[index].enabled = true;
        _currentIndex = index;
    }

    private void DisableCurrentSlot()
    {
        if (_currentIndex >= 0 && _currentIndex < _slotSelectionImages.Length)
        {
            _slotSelectionImages[_currentIndex].enabled = false;
        }
    }
}