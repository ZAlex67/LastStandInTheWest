using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    [SerializeField] protected Health _health;

    private void OnEnable()
    {
        if (_health == null)
        {
            Debug.LogError("Health reference is missing", this);
            return;
        }

        _health.OnHealthChanged += OnHealthUpdated;
        OnHealthUpdated();
    }

    private void OnDisable()
    {
        if (_health != null)
        {
            _health.OnHealthChanged -= OnHealthUpdated;
        }
    }

    protected abstract void OnHealthUpdated();
}