using UnityEngine;

[CreateAssetMenu(menuName = "Game/Wave Data", fileName = "WaveData")]
public class WaveData : ScriptableObject
{
    [SerializeField, Min(0)] private int _enemyCount = 5;
    [SerializeField, Min(0f)] private float _spawnInterval = 0.4f;
    [SerializeField] private EnemyBoss _boss;

    public int EnemyCount => _enemyCount;
    public float SpawnInterval => _spawnInterval;
    public EnemyBoss Boss => _boss;
}