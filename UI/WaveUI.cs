using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    private const string TimerPrefix = "Timer: ";
    private const string EnemyPrefix = "Enemies: ";

    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _enemyCountText;
    [SerializeField] private WaveManager _waveManager;

    private void OnEnable()
    {
        if (_waveManager == null)
        {
            return;
        }

        _waveManager.OnAllWavesCleared += ShowVictory;
        _waveManager.OnCooldownChanged += UpdateTimerText;
        _waveManager.OnEnemyCountChanged += UpdateEnemyCountText;
    }

    private void OnDisable()
    {
        if (_waveManager == null)
        {
            return;
        }

        _waveManager.OnAllWavesCleared -= ShowVictory;
        _waveManager.OnCooldownChanged -= UpdateTimerText;
        _waveManager.OnEnemyCountChanged -= UpdateEnemyCountText;
    }

    private void UpdateTimerText(float seconds)
    {
        _timerText.text = $"{TimerPrefix}{Mathf.CeilToInt(seconds)}";
    }

    private void UpdateEnemyCountText(int count)
    {
        _enemyCountText.text = $"{EnemyPrefix}{count}";
    }

    private void ShowVictory()
    {
        _timerText.text = "Victory!";
    }
}