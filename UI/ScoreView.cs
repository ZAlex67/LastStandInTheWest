using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    private const string ScorePrefix = "Score: ";

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private Health _playerHealth;

    [SerializeField] private int _enemyRegularScore = 50;
    [SerializeField] private int _enemyBossScore = 100;

    private int _score;

    private void Awake()
    {
        UpdateScoreUI();
    }

    private void OnEnable()
    {
        _waveManager.OnEnemyDied += HandleEnemyDied;
        _playerHealth.OnDamageTaken += HandlePlayerDamaged;
    }

    private void OnDisable()
    {
        _waveManager.OnEnemyDied -= HandleEnemyDied;
        _playerHealth.OnDamageTaken -= HandlePlayerDamaged;
    }

    private void HandleEnemyDied(Enemy enemy)
    {
        int score = GetEnemyScore(enemy);
        AddScore(score);
    }

    private void HandlePlayerDamaged()
    {
        TakeScore(_enemyRegularScore);
    }

    private int GetEnemyScore(Enemy enemy)
    {
        if (enemy is EnemyBoss)
        {
            return _enemyBossScore;
        }

        return _enemyRegularScore;
    }

    private void AddScore(int amount)
    {
        _score += amount;
        UpdateScoreUI();
    }

    private void TakeScore(int amount)
    {
        _score = Mathf.Max(0, _score - amount);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        _scoreText.text = $"{ScorePrefix}{_score}";
    }
}