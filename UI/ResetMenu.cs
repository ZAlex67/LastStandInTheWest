using UnityEngine;

public class ResetMenu : MonoBehaviour
{
    [SerializeField] private PauseUI _resetUI;
    [SerializeField] private PauseService _pauseService;
    [SerializeField] private Health _playerHealth;

    private void OnEnable()
    {
        _playerHealth.OnDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        _playerHealth.OnDeath -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        _pauseService.Pause();
        _resetUI.Show();
    }
}