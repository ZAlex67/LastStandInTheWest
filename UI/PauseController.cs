using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private PauseService _pauseService;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseService.Toggle();
        }
    }
}