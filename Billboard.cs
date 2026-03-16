using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;

        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }

        Debug.Assert(_mainCamera != null, "Billboard requires a camera.");
    }

    private void LateUpdate()
    {
        _transform.rotation = _mainCamera.transform.rotation;
    }
}