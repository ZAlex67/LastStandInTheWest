using UnityEngine;

public class ItemSpawnController : MonoBehaviour
{
    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private HealSpawner _healSpawner;
    [SerializeField] private AmmoSpawner _ammoSpawner;

    private static readonly AmmoType[] AmmoTypes = (AmmoType[])System.Enum.GetValues(typeof(AmmoType));

    private void Awake()
    {
        Debug.Assert(_waveManager != null);
        Debug.Assert(_healSpawner != null);
        Debug.Assert(_ammoSpawner != null);
    }

    private void OnEnable()
    {
        _waveManager.OnHealSpawned += SpawnHeal;
        _waveManager.OnAmmoSpawned += SpawnAmmo;
    }

    private void OnDisable()
    {
        _waveManager.OnHealSpawned -= SpawnHeal;
        _waveManager.OnAmmoSpawned -= SpawnAmmo;
    }

    private void SpawnHeal(Enemy enemy)
    {
        _healSpawner.Spawn(enemy.transform.position, Quaternion.identity);
    }

    private void SpawnAmmo(Enemy enemy)
    {
        Ammo ammo = _ammoSpawner.Spawn(enemy.transform.position, Quaternion.identity);
        int amount = Random.Range(5, 11);

        AmmoType randomType = GetRandomAmmoType();

        ammo.Configure(randomType, amount);
    }

    private AmmoType GetRandomAmmoType()
    {
        int randomIndex = Random.Range(0, AmmoTypes.Length);
        return AmmoTypes[randomIndex];
    }
}