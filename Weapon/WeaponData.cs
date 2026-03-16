using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private float _damage = 50f;
    [SerializeField] private float _range = 1000f;
    [SerializeField] private float _bulletSpeed = 200f;

    [SerializeField] private int _maxAmmo = 30;
    [SerializeField] private int _reloadAmount = 10;
    [SerializeField] private float _cooldown = 2f;
    [SerializeField] private int _currentAmmo = 10;

    [SerializeField] private GameObject _weaponPrefab;

    [SerializeField] private AudioClip _shootSound;

    [SerializeField] private AmmoType _ammoType;

    public float Damage => _damage;
    public float Range => _range;
    public float BulletSpeed => _bulletSpeed;
    public int MaxAmmo => _maxAmmo;
    public int ReloadAmount => _reloadAmount;
    public float Cooldown => _cooldown;
    public int CurrentAmmo => _currentAmmo;
    public GameObject WeaponPrefab => _weaponPrefab;
    public AudioClip ShootSound => _shootSound;
    public AmmoType AmmoType => _ammoType;
}