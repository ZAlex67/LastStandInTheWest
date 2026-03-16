using UnityEngine;

public class EnemyDirector : MonoBehaviour
{
    [SerializeField] private EnemyRegularSpawner _spawner;
    [SerializeField] private Transform[] _spawnPosition;

    private void Awake()
    {
        Debug.Assert(_spawnPosition != null && _spawnPosition.Length > 0);
    }

    public Enemy SpawnEnemy()
    {
        Transform spawnPoint = GetRandomSpawnPoint();
        return _spawner.Spawn(spawnPoint.position, spawnPoint.rotation);
    }

    public EnemyBoss SpawnEnemyBoss(EnemyBoss boss)
    {
        Transform spawnPoint = GetRandomSpawnPoint();
        return Instantiate(boss, spawnPoint.position, spawnPoint.rotation);
    }

    private Transform GetRandomSpawnPoint()
    {
        int index = Random.Range(0, _spawnPosition.Length);
        return _spawnPosition[index];
    }
}