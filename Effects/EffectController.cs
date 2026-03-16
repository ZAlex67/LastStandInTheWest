using UnityEngine;

[DisallowMultipleComponent]
public class EffectController : MonoBehaviour
{
    private const float DieEffectOffset = 1f;

    [SerializeField] private HitEffectSpawner _hitEffectSpawner;
    [SerializeField] private ShootEffectSpawner _shootEffectSpawner;
    [SerializeField] private EnemySpawnEffectSpawner _enemySpawnEffectSpawner;
    [SerializeField] private CollisionHitSpawner _collisionHitEffectSpawner;
    [SerializeField] private DieEffectSpawner _dieEffectSpawner;
    [SerializeField] private PlayerShooter _shooter;
    [SerializeField] private WaveManager _waveManager;

    private Weapon _currentWeapon;

    private void Awake()
    {
        Debug.Assert(_hitEffectSpawner != null, "HitEffectSpawner is missing!");
        Debug.Assert(_shootEffectSpawner != null, "ShootEffectSpawner is missing!");
        Debug.Assert(_enemySpawnEffectSpawner != null, "EnemySpawnEffectSpawner is missing!");
        Debug.Assert(_collisionHitEffectSpawner != null, "CollisionHitEffectSpawner is missing!");
        Debug.Assert(_dieEffectSpawner != null, "DieEffectSpawner is missing!");
    }

    private void OnEnable()
    {
        if (_shooter != null)
        {
            _shooter.OnWeapon += OnWeaponChanged;
        }

        if (_waveManager != null)
        {
            _waveManager.OnSpawnEffect += SpawnEnemySpawnEffect;
            _waveManager.OnSpawnDieEffect += SpawnDieEffect;
        }
    }

    private void OnDisable()
    {
        if (_shooter != null)
        {
            _shooter.OnWeapon -= OnWeaponChanged;
        }

        if (_waveManager != null)
        {
            _waveManager.OnSpawnEffect -= SpawnEnemySpawnEffect;
            _waveManager.OnSpawnDieEffect -= SpawnDieEffect;
        }

        UnsubscribeFromWeapon(_currentWeapon);
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        UnsubscribeFromWeapon(_currentWeapon);
        _currentWeapon = weapon;

        if (_currentWeapon == null)
        {
            return;
        }

        _currentWeapon.OnSpawnHitEffect += SpawnHitEffect;
        _currentWeapon.OnSpawnShootEffect += SpawnShootEffect;
        _currentWeapon.OnSpawnCollisionHitEffect += SpawnCollisionHitEffect;
    }

    private void SpawnHitEffect(Vector3 position, Quaternion rotation)
    {
        _hitEffectSpawner.Spawn(position, rotation);
    }

    private void SpawnShootEffect(Vector3 position, Quaternion rotation)
    {
        _shootEffectSpawner.Spawn(position, rotation);
    }

    private void SpawnCollisionHitEffect(Vector3 position, Quaternion rotation)
    {
        _collisionHitEffectSpawner.Spawn(position, rotation);
    }

    private void SpawnEnemySpawnEffect(Vector3 position, Quaternion rotation)
    {
        _enemySpawnEffectSpawner.Spawn(position, rotation);
    }

    private void SpawnDieEffect(Vector3 position, Quaternion rotation)
    {
        position += Vector3.up * DieEffectOffset;
        _dieEffectSpawner.Spawn(position, rotation);
    }

    private void UnsubscribeFromWeapon(Weapon weapon)
    {
        if (weapon == null)
        {
            return;
        }

        weapon.OnSpawnHitEffect -= SpawnHitEffect;
        weapon.OnSpawnShootEffect -= SpawnShootEffect;
        weapon.OnSpawnCollisionHitEffect -= SpawnCollisionHitEffect;
    }
}