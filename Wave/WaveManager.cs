using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private const float RollMax = 100f;

    [SerializeField] private List<WaveData> _waves;
    [SerializeField] private EnemyDirector _spawner;

    [SerializeField] private float _breakTime = 5f;
    [SerializeField] private float _healChance = 30f;
    [SerializeField] private float _ammoChance = 30f;

    private int _currentWave = 0;
    private int _aliveEnemies = 0;

    private bool _inBreak = true;
    private bool _isSpawning;
    private float _cooldown = 0f;

    public event Action<Enemy> OnEnemyDied;
    public event Action<float> OnCooldownChanged;
    public event Action<int> OnEnemyCountChanged;
    public event Action OnAllWavesCleared;
    public event Action<Vector3, Quaternion> OnSpawnEffect;
    public event Action<Vector3, Quaternion> OnSpawnDieEffect;
    public event Action OnEnemyDieSound;
    public event Action<Enemy> OnHealSpawned;
    public event Action<Enemy> OnAmmoSpawned;

    private void Update()
    {
        UpdateBreak();
    }

    private void UpdateBreak()
    {
        if (!_inBreak)
        {
            return;
        }

        _cooldown -= Time.deltaTime;
        OnCooldownChanged?.Invoke(_cooldown);

        if (_cooldown <= 0f && !_isSpawning)
        {
            _inBreak = false;
            StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave()
    {
        _isSpawning = true;

        if (_currentWave >= _waves.Count)
        {
            OnAllWavesCleared?.Invoke();

            yield break;
        }

        var wave = _waves[_currentWave];

        for (int i = 0; i < wave.EnemyCount; i++)
        {
            SpawnEnemy(_spawner.SpawnEnemy());

            yield return new WaitForSeconds(wave.SpawnInterval);
        }

        yield return new WaitUntil(() => _aliveEnemies <= 0);

        if (wave.Boss != null)
        {
            SpawnEnemy(_spawner.SpawnEnemyBoss(wave.Boss));

            yield return new WaitUntil(() => _aliveEnemies <= 0);
        }

        _isSpawning = false;
        _currentWave++;

        if (_currentWave >= _waves.Count)
        {
            OnAllWavesCleared?.Invoke();
        }
        else
        {
            StartBreak();
        }
    }

    private void StartBreak()
    {
        _inBreak = true;
        _cooldown = _breakTime;
    }

    private void SpawnEnemy(Enemy enemy)
    {
        enemy.Died += OnEnemyDeath;

        _aliveEnemies++;

        OnSpawnEffect?.Invoke(enemy.transform.position, enemy.transform.rotation);
        OnEnemyCountChanged?.Invoke(_aliveEnemies);
    }


    private void OnEnemyDeath(Enemy enemy)
    {
        enemy.Died -= OnEnemyDeath;

        OnSpawnDieEffect?.Invoke(enemy.transform.position, enemy.transform.rotation);
        OnEnemyDied?.Invoke(enemy);
        OnEnemyDieSound?.Invoke();

        _aliveEnemies--;

        Debug.Assert(_aliveEnemies >= 0);

        TrySpawnItem(enemy);

        OnEnemyCountChanged?.Invoke(_aliveEnemies);
    }

    private void TrySpawnItem(Enemy enemy)
    {
        float roll = UnityEngine.Random.Range(0f, RollMax);

        if (roll < _healChance)
        {
            OnHealSpawned?.Invoke(enemy);
            return;
        }

        if (roll < _healChance + _ammoChance)
        {
            OnAmmoSpawned?.Invoke(enemy);
        }
    }
}