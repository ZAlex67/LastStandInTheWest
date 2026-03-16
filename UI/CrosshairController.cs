using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private Image _crosshair;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _hitMask;

    private void Update()
    {
        UpdateCrosshair();
    }

    private void UpdateCrosshair()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, _hitMask))
        {
            _crosshair.color = Color.red;
        }
        else
        {
            _crosshair.color = Color.white;
        }
    }
}