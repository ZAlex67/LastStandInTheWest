using UnityEngine;

public class PauseService : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PauseUI _pauseUI;

    private bool _isPaused;

    public bool IsPaused => _isPaused;

    public void Toggle()
    {
        if (_isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (_isPaused)
        {
            return;
        }

        _isPaused = true;

        Time.timeScale = 0f;

        _pauseUI.Show();
        _playerController.PlayerControls.Player.Disable();

        CursorManager.UnlockCursor();
    }

    public void Resume()
    {
        if (_isPaused == false)
        {
            return;
        }

        _isPaused = false;

        Time.timeScale = 1f;

        _pauseUI.Hide();
        _playerController.PlayerControls.Player.Enable();

        CursorManager.LockCursor();
    }
}