using UnityEngine;
using UnityEngine.UI;

public class HealthBarSlider : HealthBar
{
    [SerializeField] protected Slider _slider;

    protected virtual void InitializeSlider()
    {
        _slider.maxValue = _health.MaxHealth;
    }

    protected override void OnHealthUpdated()
    {
        InitializeSlider();
        _slider.value = _health.CurrentHealth;
    }
}